using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
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

        public int PageSlotCount => 10; // 한 페이지에 표시할 아이템 개수

        private int _pageNumber = 0;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;

                if (_pageNumber < 0)
                {
                    _pageNumber = PageCount-1; // 음수 페이지는 0으로 설정
                }
                else if (_pageNumber >= PageCount)
                {
                    _pageNumber = 0;
                }
                ShowItemsUpdate(); // 페이지 변경 시 아이템 목록 업데이트
            }
        }

        public int PageCount => Items.Count > 0 ? (int)Math.Ceiling((float)Items.Count / PageSlotCount) : 1; // 페이지 개수 (10개씩 표시)


        public Shop(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new();
            Items = ItemManager.Instance.GetAllItems(); // 모든 아이템을 가져옴
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
            if (character == null|| item == null) return false;

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

        public void ShowItemsUpdate()
        {
            ShowItems = Items;
            ShowItems = ItemManager.Instance.GetItemRange(Items, PageNumber * PageSlotCount, PageSlotCount);
        }

        public string GetShopItemInfo(Item item)
        {
            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description} | {item.Price} G";
        }

    }
}
