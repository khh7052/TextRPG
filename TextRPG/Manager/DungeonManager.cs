using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Manager
{
    internal class DungeonManager
    {
        private static DungeonManager _instance;
        public static DungeonManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }

        public List<Dungeon> Dungeons { get; private set; } // 던전 목록

        private DungeonManager()
        {
            _instance = this;
            Dungeons = new List<Dungeon>();
            InitializeDungeons();
        }

        private void InitializeDungeons()
        {
            // 기본 던전 추가
            Dungeons.Add(new Dungeon("기본 던전", "초보자를 위한 기본 던전입니다.", 1, 1000, new List<string> { "슬라임", "고블린" }, 5));
            Dungeons.Add(new Dungeon("어두운 동굴", "어두운 동굴로, 강력한 몬스터가 출몰합니다.", 2, 1700, new List<string> { "박쥐", "늑대" }, 10));
            Dungeons.Add(new Dungeon("고대 유적", "고대의 유적지로, 강력한 보스가 존재합니다.", 3, 2500, new List<string> { "좀비", "스켈레톤" }, 15));
            Dungeons.Add(new Dungeon("용의 둥지", "용이 서식하는 위험한 지역입니다.", 4, 5000, new List<string> { "용", "드래곤" }, 20));
            Dungeons.Add(new Dungeon("마법사의 탑", "강력한 마법사가 지배하는 탑입니다.", 5, 7000, new List<string> { "마법사", "고블린 마법사" }, 25));
        }
    }
}
