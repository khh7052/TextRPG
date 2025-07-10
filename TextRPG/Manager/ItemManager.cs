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

        public List<Item> items { get; private set; } // 아이템 목록

        public ItemManager()
        {
            _instance = this;

            items = new List<Item>
            {
                new Item("나무검", "기본 나무로 만든 검", 100, 10, ItemType.WEAPON),
                new Item("철갑옷", "튼튼한 철로 만든 갑옷", 200, 5, ItemType.ARMOR),
                new Item("철검", "단단한 철로 만든 검", 50, 20, ItemType.WEAPON),
                new Item("백과사전", "모든 지식이 들어있는 사전", 50, 20, ItemType.WEAPON)
            };
        }

        public List<Item> GetItemRange(int start, int count)
        {
            if (start < 0 || start >= items.Count || count <= 0)
            {
                return new List<Item>();
            }

            return items.Skip(start).Take(count).ToList();
        }


        public List<Item> GetItemRange(List<Item> items, int start, int count)
        {
            if (start < 0 || start >= items.Count || count <= 0)
            {
                return new List<Item>();
            }

            return items.Skip(start).Take(count).ToList();
        }
    }
}
