using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;

namespace TextRPG.Scene
{
    internal class DungeonScene : SceneBase
    {
        public enum DungeonMenuType
        {
            LOBBY, // 로비
            CLEAR, // 던전 성공
            FAIL, // 던전 실패
        }

        public DungeonMenuType MenuType { get; set; } = DungeonMenuType.LOBBY; // 현재 메뉴 상태

        public Dungeon CurrentDungeon { get; set; } // 현재 선택된 던전

        private bool _hasExploredDungeon = false; // 던전 탐험 여부

        private ExploredData _exploredData; // 던전 탐험 데이터 저장

        // 던전 탐험 데이터 구조체
        public struct ExploredData
        {
            public float previousHP; // 던전 탐험 전 체력
            public float currentHP; // 던전 탐험 후 체력
            public int previousGold; // 던전 탐험 전 골드
            public int currentGold; // 던전 탐험 후 골드
            public int previousExperience; // 던전 탐험 전 경험치
            public int currentExperience; // 경험치
        }


        public DungeonScene()
        {
            Name = "⚔️ 던전";
            Description = "던전을 탐험하며 몬스터와 싸우고 보물을 찾을 수 있습니다.";


            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () =>
            {
                if(MenuType == DungeonMenuType.LOBBY)
                    SceneManager.ChangeScene(SceneType.START);
                else
                    MenuType = DungeonMenuType.LOBBY; // 로비로 돌아가기
            }));

            // 던전 목록을 표시하기 위한 메뉴 추가
            for (int i = 0; i < DungeonManager.Instance.Dungeons.Count; i++)
            {
                Dungeon dungeon = DungeonManager.Instance.Dungeons[i];
                DungeonMenu dungeonMenu = new DungeonMenu(this, dungeon);
                ItemMenus.Add(dungeonMenu);
            }

        }

        public override void MainDisplay()
        {
            switch (MenuType)
            {
                case DungeonMenuType.LOBBY:
                    ItemMenuDisplay();
                    break;
                case DungeonMenuType.CLEAR:
                    MainDisplay_Clear();
                    break;
                case DungeonMenuType.FAIL:
                    MainDisplay_Fail();
                    break;
            }
        }

        public override void Init()
        {
            switch (MenuType)
            {
                case DungeonMenuType.LOBBY:
                    Name = "⚔️ 던전";
                    Description = "던전을 탐험하며 몬스터와 싸우고 보물을 찾을 수 있습니다.";

                    foreach (var menu in ItemMenus)
                        menu.Enable = true;
                    break;
                case DungeonMenuType.CLEAR:
                    Name = "⚔️ 던전 - 탐사 성공";
                    Description = $"{CurrentDungeon.Name} 탐사를 성공하였습니다.";

                    foreach (var menu in ItemMenus)
                        menu.Enable = false;
                    break;
                case DungeonMenuType.FAIL:
                    Name = $"⚔️ 던전 - 탐사 실패";
                    Description = $"{CurrentDungeon.Name} 탐사를 실패하였습니다...";

                    foreach (var menu in ItemMenus)
                        menu.Enable = false;
                    break;
            }
        }

        public void ExploreDungeon(Dungeon dungeon)
        {
            _hasExploredDungeon = false;
            Character player = GameManager.Player;
            CurrentDungeon = dungeon;

            if (player.DEF < CurrentDungeon.RecommendedDefense)
            {
                Random random = new();
                int clearRand = random.Next(1, 101); // 1부터 100까지의 랜덤 숫자 생성
                MenuType = clearRand <= 40 ? DungeonMenuType.FAIL : DungeonMenuType.CLEAR; // 40% 확률로 실패, 나머지 60% 확률로 성공
            }
            else
            {
                MenuType = DungeonMenuType.CLEAR; // 성공
            }
        }

        public void MainDisplay_Clear()
        {
            Character player = GameManager.Player;
            if (!_hasExploredDungeon)
            {
                float penaltyHP = CurrentDungeon.GetDungeonPenalty_HP();
                int reward = CurrentDungeon.GetDungeonPlusReward_Gold(); // 골드 보상 추가

                _exploredData = new ExploredData();
                _exploredData.previousHP = player.HP; // 탐험 전 체력
                _exploredData.currentHP = player.HP - penaltyHP; // 탐험 후 체력
                _exploredData.previousGold = player.Gold; // 탐험 전 골드
                _exploredData.currentGold = player.Gold + reward; // 탐험 후 골드
                _exploredData.previousExperience = player.Experience; // 탐험 전 경험치
                _exploredData.currentExperience = player.Experience + 1; // 탐험 후 경험치 증가

                player.HP = (int)_exploredData.currentHP; // 체력 업데이트
                player.Gold = _exploredData.currentGold; // 골드 업데이트
                player.Experience = _exploredData.currentExperience; // 경험치 업데이트

                _hasExploredDungeon = true;
            }

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {_exploredData.previousHP} -> {_exploredData.currentHP}");
            Console.WriteLine($"Gold {_exploredData.previousGold} G -> {_exploredData.currentGold} G");
        }

        public void MainDisplay_Fail()
        {
            Character player = GameManager.Player;
            if (!_hasExploredDungeon)
            {
                float penaltyHP = CurrentDungeon.GetDungeonPenalty_HP();

                _exploredData = new ExploredData();
                _exploredData.previousHP = player.HP; // 탐험 전 체력
                _exploredData.currentHP = player.HP * 0.5f - penaltyHP; // 탐험 후 체력 50% 감소 + 패널티 체력 감소
                _exploredData.previousGold = player.Gold; // 탐험 전 골드
                _exploredData.currentGold = player.Gold; // 탐험 후 골드
                _exploredData.previousExperience = player.Experience; // 탐험 전 경험치
                _exploredData.currentExperience = player.Experience; // 탐험 후 경험치 증가

                _hasExploredDungeon = true;
            }


            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {_exploredData.previousHP} -> {_exploredData.currentHP}");
            player.HP = _exploredData.currentHP;
        }
    }
}