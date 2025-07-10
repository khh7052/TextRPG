using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.MenuCollections
{
    internal class ShopMenu : Menu
    {
        public Item Item { get; private set; }

        public Shop Shop { get; private set; } // 상점 정보

        public ShopMenu(Shop shop, Item item)
        {
            Shop = shop;
            Item = item;
        }

        public override void Display()
        {
            if (!Enable) return;

            if (isSelected)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black; // 선택되지 않은 메뉴는 검은색 배경으로 표시
                Console.ForegroundColor = Color;
            }

            Content = GetItemInfo(Item);
            string content = isSelected ? $"▶   {Content}" : Content;
            Console.WriteLine(content);
            Console.ResetColor(); // 색상 초기화
        }

        public static string GetItemInfo(Item item)
        {
            if (item == null) return "                   ";

            string icon = Item.GetItemIcon(item.Type);
            string name = $"{icon} {item.Name}".PadRight(14);       // 이름 필드 (14칸)
            string effect = $"{Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue}".PadRight(14); // 효과 필드 (14칸)
            string description = item.Description.PadRight(14);
            string gold = $"{item.Price} Gold".PadRight(14); // 가격 필드 (14칸)

            return $"{name}| {effect}| {description}| {gold}";
        }

        public void Buy()
        {
            if (Item == null) return;

            // 똑같은 아이템이 있으면 경고
            if (GameManager.Player.HasItem(Item))
            {
                GameManager.DisplayWarning($"{Item.Name} 아이템은 이미 보유하고 있습니다. 다른 아이템을 선택해주세요.");
                return;
            }

            // 구매 가능하면 구매
            if (Shop.BuyItem(GameManager.Player, Item))
                GameManager.DisplayMessage($"{Item.Name} 아이템을 구매했습니다.");
            // 구매 불가능하면 경고
            else
                GameManager.DisplayWarning("구매에 실패했습니다. 골드가 부족합니다.");
        }

        public void Sell()
        {
            // 구매 가능하면 구매
            if (Shop.SellItem(GameManager.Player, Item))
                GameManager.DisplayMessage($"{Item.Name} 아이템을 판매했습니다. + {Shop.GetSellGold(Item)} G");
            // 구매 불가능하면 경고
            else
                GameManager.DisplayWarning("판매에 실패했습니다. 인벤토리내에 아이템이 존재하지 않습니다.");
        }
    }
}
