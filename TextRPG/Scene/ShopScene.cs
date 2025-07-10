using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;

namespace TextRPG.Scene
{
    internal class ShopScene : SceneBase
    {
        public enum ShopMenuType
        {
            LOBBY = 0, // 로비
            BUY = 1, // 아이템 구매
            SELL = 2, // 아이템 판매
        }

        public Shop Shop { get; set; } // 상점 인스턴스 생성

        public ShopMenuType MenuType { get; set; } = ShopMenuType.LOBBY;

        public ShopScene()
        {
            
            Shop = new("상점", "아이템을 구매하거나 판매할 수 있는 곳입니다.");
            Name = Shop.Name;
            Description = Shop.Description;
             
            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
            SelectMenus.Add(new Menu("🛒 아이템 구매", ConsoleColor.Cyan, () => MenuType = ShopMenuType.BUY));
            SelectMenus.Add(new Menu("💵 아이템 판매", ConsoleColor.Cyan, () => MenuType = ShopMenuType.SELL));
             
            UpdateItemMenu();
            for (int i = 0; i < 10; i++)
            {
                Item item = Shop.ShowItems != null && i < Shop.ShowItems.Count ? Shop.ShowItems[i] : null;
                ItemMenus.Add(new ShopMenu(Shop, item));
            }
        }

        public override void Init()
        {
            switch (MenuType)
            {
                case ShopMenuType.LOBBY:
                    Name = Shop.Name;
                    Description = Shop.Description;
                    break;
                case ShopMenuType.BUY:
                    Name = Shop.Name + " - 아이템 구매";
                    Description = "아이템을 구매할 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        ShopMenu shopMenu = ItemMenus[i] as ShopMenu;
                        if (shopMenu != null)
                        {
                            shopMenu.OnSelect = () => shopMenu.Buy(); // 구매 메소드 설정
                        }
                    }
                    break;
                case ShopMenuType.SELL:
                    Name = Shop.Name + " - 아이템 판매";
                    Description = "아이템을 판매할 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        ShopMenu shopMenu = ItemMenus[i] as ShopMenu;
                        if (shopMenu != null)
                        {
                            shopMenu.OnSelect = () => shopMenu.Sell(); // 판매 메소드 설정
                        }
                    }
                    break;
            }
        }

        public override void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            GameManager.ColorWriteLine($"🛒 {Name}", nameColor);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine(Description);
            Console.WriteLine();
        }

        public override void MainDisplay()
        {
            Console.WriteLine("[보유 골드]");
            GameManager.ColorWriteLine($"{GameManager.Player.Gold} G", ConsoleColor.Yellow);
            Console.WriteLine();
            ItemMenuDisplay();
        }

        public override void ItemMenuDisplay()
        {
            Console.WriteLine("───── 보유 장비 ─────");
            ItemMenuDisplayMethod();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }

        void UpdateItemMenu()
        {
            Shop.ShowItemsUpdate();

            if (Shop.ShowItems == null || Shop.ShowItems.Count == 0)
                return;

            int count = 0;
            for (int i = 0; i < Shop.ShowItems.Count; i++)
            {
                for (int j = count; j < ItemMenus.Count; j++)
                {
                    ToggleItemMenu toggleItemMenu = ItemMenus[j] as ToggleItemMenu;

                    if (toggleItemMenu != null)
                    {
                        toggleItemMenu.Item = Shop.ShowItems[i]; // 아이템 설정
                        count++;
                        break;
                    }
                }
            }

        }

    }
}
