using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.Scene.DungeonScene;
using TextRPG.Manager;
using TextRPG.Scene;

namespace TextRPG.MenuCollections
{
    internal class DungeonMenu : Menu
    {
        public DungeonScene MyScene { get; set; } // 현재 씬 정보
        public Dungeon Dungeon { get; set; } // 던전 정보
        public DungeonMenu(DungeonScene dungeonScene, Dungeon dungeon)
        {
            MyScene = dungeonScene; // 현재 씬 설정
            Dungeon = dungeon;
            OnSelect = () =>
            {
                if (Dungeon != null)
                {
                    MyScene.ExploreDungeon(Dungeon); // 던전 탐험 메소드 호출
                }
            };
        }

        public override void Display()
        {
            if (!Enable) return;

            Console.BackgroundColor = isSelected ? ConsoleColor.DarkGray : ConsoleColor.Black; // 선택된 메뉴는 어두운 회색 배경
            Console.ForegroundColor = isSelected ? ConsoleColor.White : Color; // 메뉴 색상 설정

            // 메뉴 내용 출력
            Content = GetDungeonInfo();
            string content = isSelected ? $"▶   {Content}" : Content;
            Console.WriteLine(content); // 메뉴 내용 출력

            Console.ResetColor(); // 색상 초기화
        }

        public string GetDungeonInfo()
        {
            if (Dungeon == null) return "";

            string info = $"-🏰 {Dungeon.Name} | (레벨: {Dungeon.Level} | 보상: {Dungeon.RewardGold} 골드 | 추천 방어력: {Dungeon.RecommendedDefense})";
            return info;
        }
    }
}
