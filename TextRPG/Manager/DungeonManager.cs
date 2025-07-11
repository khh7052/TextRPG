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
            Dungeons.Add(new Dungeon("초원지대", "초보 모험가들이 첫발을 내딛는 평화로운 초원입니다.", 1, 10, new List<string> { "슬라임", "토끼", "벌레" }, 5));
            Dungeons.Add(new Dungeon("고목숲", "무성한 나무가 우거진 숲으로 중간 난이도의 몬스터가 서식합니다.", 5, 30, new List<string> { "고블린", "늑대", "늑대인간" }, 12));
            Dungeons.Add(new Dungeon("붉은협곡", "화산 지대가 펼쳐진 붉은 협곡, 강력한 몬스터들이 출몰합니다.", 10, 90, new List<string> { "용암거북이", "화염정령", "화염드래곤" }, 18));
            Dungeons.Add(new Dungeon("고대유적", "잊혀진 고대의 유적, 강력한 보스와 몬스터가 기다립니다.", 15, 200, new List<string> { "스켈레톤전사", "좀비마법사", "대장스켈레톤" }, 25));
            Dungeons.Add(new Dungeon("심연의탑", "최종 보스가 숨어있는 위험한 심연의 탑입니다.", 20, 400, new List<string> { "심연의군주", "다크엘프", "고대용" }, 35));
        }
    }
}
