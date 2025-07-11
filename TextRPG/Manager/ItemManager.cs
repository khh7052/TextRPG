using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Manager
{
    internal class ItemManager
    {
        private static ItemManager _instance;
        public static ItemManager Instance
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

        public Dictionary<string, Item> ItemDictionary { get; set; }

        // public List<Item> Items { get; private set; } // 아이템 목록

        private Random random = new Random(); // 랜덤 아이템 생성을 위한 인스턴스

        public ItemManager()
        {
            _instance = this;

            ItemDictionary = new Dictionary<string, Item>();
            InitializeItems();
        }
        private void InitializeItems()
        {
            // 무기 (가격 오름차순)
            AddItem(new Item("낡은 검", "시간이 지나 많이 무뎌진 검입니다.", 50, 2, ItemType.WEAPON));
            AddItem(new Item("청동 단검", "짧고 날카로운 청동 단검입니다.", 80, 3, ItemType.WEAPON));
            AddItem(new Item("강철 롱소드", "균형 잡힌 강철 재질의 롱소드입니다.", 200, 8, ItemType.WEAPON));
            AddItem(new Item("엘프 활", "정밀한 엘프 제작 활로 정확도가 높습니다.", 250, 10, ItemType.WEAPON));
            AddItem(new Item("은빛 단검", "얇고 가벼운 은 재질의 단검입니다.", 270, 11, ItemType.WEAPON));
            AddItem(new Item("단단한 창", "기본에 충실한 무난한 창입니다.", 300, 12, ItemType.WEAPON));
            AddItem(new Item("청강검", "단단한 청강석으로 만들어진 검입니다.", 320, 13, ItemType.WEAPON));
            AddItem(new Item("흑철 도끼", "묵직한 한 방을 날릴 수 있는 도끼입니다.", 380, 15, ItemType.WEAPON));
            AddItem(new Item("화염 마검", "불꽃의 힘이 깃든 마법의 검입니다.", 400, 16, ItemType.WEAPON));
            AddItem(new Item("전장의 검", "전투에 최적화된 균형 잡힌 검입니다.", 430, 17, ItemType.WEAPON));
            AddItem(new Item("장인의 롱소드", "정밀하게 세공된 고급 롱소드입니다.", 500, 20, ItemType.WEAPON));
            AddItem(new Item("무쇠망치", "무거운 무쇠로 만들어진 전투 망치입니다.", 550, 22, ItemType.WEAPON));
            AddItem(new Item("용의 송곳니", "전설 속 용의 송곳니를 깎아 만든 단검입니다.", 600, 24, ItemType.WEAPON));
            AddItem(new Item("심연의 낫", "어둠의 힘을 담은 사신의 낫입니다.", 900, 36, ItemType.WEAPON));

            // 방어구 (가격 오름차순)
            AddItem(new Item("가죽 갑옷", "얇은 가죽으로 만든 기본 방어구입니다.", 50, 2, ItemType.ARMOR));
            AddItem(new Item("청동 방패", "적당한 방어력을 지닌 청동 방패입니다.", 100, 3, ItemType.ARMOR));
            AddItem(new Item("강철 갑옷", "단단한 강철로 제작된 중형 갑옷입니다.", 220, 7, ItemType.ARMOR));
            AddItem(new Item("엘프 망토", "은밀하게 움직일 수 있도록 돕는 망토입니다.", 280, 8, ItemType.ARMOR));
            AddItem(new Item("강화 가죽갑옷", "가죽에 철심을 덧댄 가벼운 갑옷입니다.", 300, 9, ItemType.ARMOR));
            AddItem(new Item("청강 갑옷", "청강석으로 제작된 단단한 갑옷입니다.", 320, 10, ItemType.ARMOR));
            AddItem(new Item("사슬 갑옷", "금속 사슬로 이루어진 중형 방어구입니다.", 350, 11, ItemType.ARMOR));
            AddItem(new Item("은빛 갑옷", "은으로 만들어져 마법 저항이 높습니다.", 350, 11, ItemType.ARMOR));
            AddItem(new Item("튼튼한 갑옷", "방어력과 이동력의 균형을 맞춘 갑옷입니다.", 400, 12, ItemType.ARMOR));
            AddItem(new Item("중갑 세트", "전장을 위한 무거운 중갑 세트입니다.", 450, 14, ItemType.ARMOR));
            AddItem(new Item("화염 방어구", "불에 강한 마법이 부여된 갑옷입니다.", 450, 14, ItemType.ARMOR));
            AddItem(new Item("대지의 방패", "적의 공격을 튕겨낼 수 있는 강력한 방패입니다.", 500, 15, ItemType.ARMOR));
            AddItem(new Item("전장의 갑옷", "전투 경험이 많은 병사들이 애용하는 갑옷입니다.", 500, 15, ItemType.ARMOR));
            AddItem(new Item("빛의 로브", "마법사를 위한 신성한 방어 로브입니다.", 650, 20, ItemType.ARMOR));
            AddItem(new Item("용비늘 갑옷", "드래곤의 비늘로 만들어진 전설의 갑옷입니다.", 700, 21, ItemType.ARMOR));
            AddItem(new Item("심연의 망토", "어둠을 흡수하는 망토로 적의 시야를 방해합니다.", 950, 29, ItemType.ARMOR));
        }


        private void AddItem(Item item)
        {
            if (!ItemDictionary.ContainsKey(item.Name))
            {
                // Items.Add(item);
                ItemDictionary.Add(item.Name, item);
            }
        }

        public List<Item> GetRandomItems(int count)
        {
            return ItemDictionary.Values.OrderBy(x => random.Next()).Take(count).ToList();
        }


        public List<Item> GetAllItems()
        {
            return ItemDictionary.Values.ToList(); // 값 복사된 리스트 반환
        }

        public List<Item> GetAllItemClones()
        {
            return ItemDictionary.Values.Select(item => item.Clone()).ToList(); // 값 복사된 리스트 반환
        }

        public List<Item> GetItemRange(int start, int count)
        {
            if (start < 0 || start >= ItemDictionary.Values.Count || count <= 0)
                return new();

            return ItemDictionary.Values.Skip(start).Take(count).ToList();
        }


        public List<Item> GetItemRange(List<Item> items, int start, int count)
        {
            if (start < 0 || start >= items.Count || count <= 0) 
                return new();

            return items.Skip(start).Take(count).ToList();
        }
    }
}
