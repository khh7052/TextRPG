using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG
{
    internal class Dungeon
    {
        public string Name { get; set; } // 던전 이름
        public string Description { get; set; } // 던전 설명
        public string Paint { get; set; } // 던전 그림
        public ConsoleColor PaintColor { get; set; } = ConsoleColor.White; // 던전 그림 색상
        public string Icon { get; set; } = "🏰"; // 던전 아이콘

        public int Level { get; set; } // 던전 레벨
        public int RewardGold { get; set; } // 던전 클리어 보상 금액
        public List<string> Monsters { get; set; } // 던전에서 만날 수 있는 몬스터 목록
        public int RecommendedDefense { get; set; } // 추천 방어력

        public Dungeon()
        {
            Name = "기본 던전";
            Description = "기본 던전 설명입니다.";
            Paint = """
                                         .        .         .     .      .       
                      .       .       .      .        .    .    
                             .    .       .    .  .         .   
                     ~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~    
                     / \     / \   / \     / \   / \     / \     
                    /   \   /   \ /   \   /   \ /   \   /   \    
                   /     \_/     \     \_/     \     \_/     \   
                  /                                     .    . \  
                 /        .      .        .       .        .    \ 
                /_______________________________________________\

                """;
            Level = 1;
            RewardGold = 100;
            Monsters = new List<string> { "슬라임", "고블린" };
            RecommendedDefense = 5;
        }

        public Dungeon(string name, string description, int level, int rewardGold, List<string> monsters, int recommendedDefense, string paint = "", ConsoleColor paintColor = ConsoleColor.White, string icon = "🏰")
        {
            Name = name;
            Description = description;
            Level = level;
            RewardGold = rewardGold;
            Monsters = monsters;
            RecommendedDefense = recommendedDefense;
            Paint = paint;
            PaintColor = paintColor;
            Icon = icon;
        }


        public int GetDungeonPenalty_HP()
        {
            Random random = new();
            int penalty = random.Next(20, 36); // 20 ~ 35
            penalty -= (int)GameManager.Player.DEF - RecommendedDefense; // 방어력에 따라 감소
            if (penalty < 0) penalty = 0; // 최소 0으로 설정
            return penalty;
        }

        public int GetDungeonPlusReward_Gold()
        {
            float atk = GameManager.Player.ATK;
            Random random = new();
            float percent = random.Next((int)atk, (int)atk * 2) * 0.01f;

            return RewardGold + (int)(RewardGold * percent);
        }
    }
}
