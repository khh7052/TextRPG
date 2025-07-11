using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.Scene;

namespace TextRPG.MenuCollections
{
    internal class ShopMenu : Menu
    {
        public Item Item { get; set; }

        public Shop Shop { get; private set; } // 상점 정보

        public ShopScene MyScene { get; set; }

        public ShopMenu(Shop shop, Item item)
        {
            Shop = shop;
            Item = item;
        }

        public override void Display()
        {
            if (!Enable) return;

            if (IsSelected)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                if(MyScene.MenuType == ShopScene.ShopMenuType.BUY)
                {
                    if (GameManager.Player.HasItem(Item))
                        Console.ForegroundColor = ConsoleColor.DarkGray; // 이미 보유한 아이템은 회색으로 표시
                    else
                        Console.ForegroundColor = Color;
                }
                else
                {
                    if (GameManager.Player.HasEquippedItem(Item))
                        Console.ForegroundColor = ConsoleColor.Yellow; // 이미 장착된 아이템은 노란색으로 표시
                    else
                        Console.ForegroundColor = Color; // 기본 색상 사용
                }
                Console.BackgroundColor = ConsoleColor.Black; // 선택되지 않은 메뉴는 검은색 배경으로 표시
            }

            Content = MyScene.MenuType == ShopScene.ShopMenuType.BUY ? GetBuyItemInfo(Item) : GetSellItemInfo(Item);
            string content = IsSelected ? $"▶   {Content}" : Content;
            Console.WriteLine(content);
            Console.ResetColor(); // 색상 초기화
        }

        public string GetBuyItemInfo(Item item)
        {
            if (item == null) return "                   ";

            string icon = Item.GetItemIcon(item.Type);
            string name = $"{icon} {item.Name}".PadRight(14);
            string effect = $"{Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue}".PadRight(14);
            string description = item.Description.PadRight(14);
            string gold = $"{item.Price} Gold".PadRight(14);

            return $"{name}| {effect}| {description}| {gold}";
        }


        public string GetSellItemInfo(Item item)
        {
            if (item == null) return "                   ";

            string icon = Item.GetItemIcon(item.Type);
            string name = $"{icon} {item.Name}".PadRight(14);       // 이름 필드 (14칸)
            string effect = $"{Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue}".PadRight(14); // 효과 필드 (14칸)
            string description = item.Description.PadRight(14);
            string gold = $"{Shop.GetSellGold(item)} Gold".PadRight(14); // 가격 필드 (14칸)

            bool hasEquippedItem = GameManager.Player.HasEquippedItem(item);
            string equippedStatus = hasEquippedItem ? "✅" : ""; // 장착 여부 표시

            return $"{equippedStatus} {name}| {effect}| {description}| {gold}";
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
            if (Item == null) return;

            // 구매 가능하면 구매
            if (Shop.SellItem(GameManager.Player, Item))
                GameManager.DisplayMessage($"{Item.Name} 아이템을 판매했습니다. + {Shop.GetSellGold(Item)} G");
            // 구매 불가능하면 경고
            else
                GameManager.DisplayWarning("판매에 실패했습니다. 인벤토리내에 아이템이 존재하지 않습니다.");
        }
    }
}
