using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Dungeon
    {
        public string Name { get; set; } // 던전 이름
        public string Description { get; set; } // 던전 설명
        public int Level { get; set; } // 던전 레벨 (난이도)
        public int RewardGold { get; set; } // 던전 클리어 보상 금액
        public List<string> Monsters { get; set; } // 던전에서 만날 수 있는 몬스터 목록
        public int RecommendedDefense { get; set; } // 추천 방어력 (던전 난이도에 따라 설정)

        public Dungeon()
        {
            Name = "기본 던전";
            Description = "기본 던전 설명입니다.";
            Level = 1;
            RewardGold = 100;
            Monsters = new List<string> { "슬라임", "고블린" };
            RecommendedDefense = 5;
        }

        public Dungeon(string name, string description, int level, int rewardGold, List<string> monsters, int recommendedDefense)
        {
            Name = name;
            Description = description;
            Level = level;
            RewardGold = rewardGold;
            Monsters = monsters;
            RecommendedDefense = recommendedDefense;
        }
    }
}
