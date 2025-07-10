using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG
{
    internal class Shop
    {
        public string Name { get; set; } // 상점 이름
        public string Description { get; set; } // 상점 설명
        public List<Item> Items { get; private set; } // 상점 아이템 목록

        public List<Item> ShowItems { get; private set; } // 현재 표시할 아이템 목록

        public Shop(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();

            // 상점에 기본 아이템 추가
            Items.Add(new Item("나무", "기본 나무로 만든 검", 100, 10, ItemType.WEAPON));
            Items.Add(new Item("철", "튼튼한 철로 만든 갑옷", 200, 5, ItemType.ARMOR));
            Items.Add(new Item("검", "단단한 철로 만든 검", 50, 20, ItemType.WEAPON));
            Items.Add(new Item("사전", "모든 지식이 들어있는 사전", 50, 20, ItemType.WEAPON));
        }

        // 상점에 아이템 추가
        public void AddItem(Item item)
        {
            if (item != null && !Items.Contains(item))
            {
                Items.Add(item);
            }
        }
        // 상점에서 아이템 제거
        public void RemoveItem(Item item)
        {
            if (item != null && Items.Contains(item))
            {
                Items.Remove(item);
            }
        }

        // 상점에서 아이템 구매
        public bool BuyItem(Character character, Item item)
        {
            if (character.Gold >= item.Price && Items.Contains(item))
            {
                character.Gold -= item.Price;
                character.AddItem(item); // 캐릭터 인벤토리에 아이템 추가
                // RemoveItem(item);
                return true; // 구매 성공
            }

            return false; // 구매 실패
        }

        // 상점에서 아이템 판매
        public bool SellItem(Character character, Item item)
        {
            if (character.HasItem(item))
            {
                character.Gold += GetSellGold(item); // 아이템 가격만큼 골드 증가
                character.RemoveItem(item); // 캐릭터 인벤토리에서 아이템 제거
                // AddItem(item);
                return true; // 판매 성공
            }

            return false; // 판매 실패
        }

        public int GetSellGold(Item item)
        {
            if (item == null) return 0; // 아이템이 null인 경우 0 반환
            return (int)(item.Price * 0.85f); // 판매 시 아이템 가격의 절반을 반환
        }

        public void DisplayShopItemList(bool displayNumber)
        {
            ShowItems = ItemManager.Instance.GetItemRange(Items, 0, 10); // 예시로 10개 아이템만 가져옴
            if (ShowItems == null || ShowItems.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
                return;
            }

            for (int i = 0; i < ShowItems.Count; i++)
            {
                var item = ShowItems[i];
                Console.ForegroundColor = item.Type == ItemType.WEAPON ? ConsoleColor.Cyan : ConsoleColor.Green;
                string itemInfo = GetShopItemInfo(item);
                string itemNumber = displayNumber ? $"{i + 1}. " : "";

                if (GameManager.Player.HasItem(item))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // 이미 구매한 아이템은 노란색으로 표시
                    Console.WriteLine($"{itemNumber}{itemInfo} | 구매완료");
                }
                else
                {
                    Console.WriteLine($"{itemNumber}{itemInfo}");
                }
            }
            Console.ResetColor();
        }

        public string GetShopItemInfo(Item item)
        {
            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description} | {item.Price} G";
        }


        public void DisplayInventoryItemList()
        {
            ShowItems = ItemManager.Instance.GetItemRange(GameManager.Player.Inventory, 0, 10); // 예시로 10개 아이템만 가져옴
            if (ShowItems == null || ShowItems.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
                return;
            }

            for (int i = 0; i < ShowItems.Count; i++)
            {
                var item = ShowItems[i];
                Console.ForegroundColor = item.Type == ItemType.WEAPON ? ConsoleColor.Cyan : ConsoleColor.Green;
                string itemInfo = GetShopSellItemInfo(item);

                if (GameManager.Player.HasEquippedItem(item))
                {
                    Console.WriteLine($"{i + 1}. {itemInfo} | 장착중");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {itemInfo}");
                }
            }
            Console.ResetColor();
        }


        public string GetShopSellItemInfo(Item item)
        {
            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description} | {GetSellGold(item)} G";
        }

    }
}
