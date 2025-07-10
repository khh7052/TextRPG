using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using static TextRPG.Scene.ShopScene;

namespace TextRPG.Scene
{
    internal class DungeonScene : SceneBase
    {
        public enum DungeonMenu
        {
            LOBBY, // 로비
            CLEAR, // 던전 성공
            FAIL, // 던전 실패
        }

        public DungeonMenu Menu { get; set; } = DungeonMenu.LOBBY; // 현재 메뉴 상태

        public Dungeon CurrentDungeon { get; set; } // 현재 선택된 던전

        public DungeonScene()
        {
            Name = "던전";
            Description = "던전을 탐험하며 몬스터와 싸우고 보물을 찾을 수 있습니다.";
        }

        public override void MainDisplay()
        {
            switch (Menu)
            {
                case DungeonMenu.LOBBY:
                    MainDisplay_Lobby();
                    break;
                case DungeonMenu.CLEAR:
                    MainDisplay_Clear();
                    break;
                case DungeonMenu.FAIL:
                    MainDisplay_Fail();
                    break;
            }
        }

        public override void SelectMenu(int selection)
        {
            switch (Menu)
            {
                case DungeonMenu.LOBBY:
                    SelectMenu_Lobby(selection);
                    break;
                case DungeonMenu.CLEAR:
                    SelectMenu_Clear(selection);
                    break;
                case DungeonMenu.FAIL:
                    SelectMenu_Fail(selection);
                    break;
            }
        }


        public override void Init()
        {
            switch (Menu)
            {
                case DungeonMenu.LOBBY:
                    Name = "던전입장";
                    Description = "여기서 던전을 선택하고 탐험을 시작할 수 있습니다.";
                    break;
                case DungeonMenu.CLEAR:
                    Name = "던전 - 탐사 성공";
                    Description = $"{CurrentDungeon.Name} 탐사를 성공하였습니다.";
                    break;
                case DungeonMenu.FAIL:
                    Name = $"던전 - 탐사 실패";
                    Description = $"{CurrentDungeon.Name} 탐사를 실패하였습니다...";
                    break;
            }
        }


        public void MainDisplay_Lobby()
        {
            Console.WriteLine("[던전 목록]");
            DungeonManager.Instance.DisplayDungeonList();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public void MainDisplay_Clear()
        {
            Character player = GameManager.Player;

            float penaltyHP = GetDungeonPenalty_HP();
            int reward = GetDungeonPlusReward_Gold(); // 골드 보상 추가

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {player.HP} -> {player.HP - penaltyHP}");
            Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + reward} G");
            Console.WriteLine("0. 나가기");

            player.HP -= penaltyHP; // 체력 감소
            player.Gold += reward; // 골드 추가
            player.Experience++; // 경험치 증가
        }

        public void MainDisplay_Fail()
        {
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {GameManager.Player.HP} -> {GameManager.Player.HP * 0.5f}");
            GameManager.Player.HP = (int)(GameManager.Player.HP * 0.5f); // 체력 50% 감소
            Console.WriteLine("0. 나가기");
        }

        public void SelectMenu_Lobby(int selection)
        {

            if (selection == 0)
            {
                SceneManager.ChangeScene(SceneType.START); // 시작 씬으로 돌아가기
                return;
            }

            // 입력 확인
            if (selection < 1 || selection > DungeonManager.Instance.Dungeons.Count)
            {
                GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                return;
            }

            CurrentDungeon = DungeonManager.Instance.Dungeons[selection - 1];
            ExploreDungeon(); // 던전 탐험 시작
        }

        public void SelectMenu_Clear(int selection)
        {
            switch (selection)
            {
                case 0:
                    Menu = DungeonMenu.LOBBY; // 로비로 돌아가기
                    break;
                default:
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    break;
            }
        }

        public void SelectMenu_Fail(int selection)
        {
            switch (selection)
            {
                case 0:
                    Menu = DungeonMenu.LOBBY; // 로비로 돌아가기
                    break;
                default:
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    break;
            }
        }

        // 던전 탐험 시작
        public void ExploreDungeon()
        {
            Character player = GameManager.Player;

            if(player.DEF < CurrentDungeon.RecommendedDefense)
            {
                Random random = new();
                int clearRand = random.Next(1, 101); // 1부터 100까지의 랜덤 숫자 생성
                Menu = clearRand <= 40 ? DungeonMenu.FAIL : DungeonMenu.CLEAR; // 40% 확률로 실패, 나머지 60% 확률로 성공
            }
            else
            {
                Menu = DungeonMenu.CLEAR; // 성공
            }
        }

        int GetDungeonPenalty_HP()
        {
            Random random = new();
            int penalty = random.Next(20, 36); // 20 ~ 35
            penalty -= (int)GameManager.Player.DEF - CurrentDungeon.RecommendedDefense; // 방어력에 따라 감소
            if (penalty < 0) penalty = 0; // 최소 0으로 설정
            return penalty;
        }

        int GetDungeonPlusReward_Gold()
        {
            float atk = GameManager.Player.ATK;
            Random random = new();
            float percent = random.Next((int)atk, (int)atk * 2) * 0.01f;

            return CurrentDungeon.RewardGold + (int)(CurrentDungeon.RewardGold * percent);
        }

        // 던전 클리어
        public void ClearDungeon()
        {
            // 보상 로직 추가
            GameManager.Player.LevelUp();
            Menu = DungeonMenu.LOBBY; // 로비로 돌아가기
        }
    }
}