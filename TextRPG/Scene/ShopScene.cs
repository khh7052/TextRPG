using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
            BUY = 1, // 아이템 구매
            SELL = 2, // 아이템 판매
        }

        public Shop Shop { get; set; } // 상점 인스턴스 생성

        public ShopMenuType MenuType { get; set; } = ShopMenuType.BUY;

        public ShopScene()
        {
            
            Shop = new("상점", "아이템을 구매하거나 판매할 수 있는 곳입니다.");
            Name = Shop.Name;
            Description = Shop.Description;

            SelectMenus.Add(new Menu("▶ 다음 페이지", ConsoleColor.Cyan, () => Shop.PageNumber++));
            SelectMenus.Add(new Menu("◀ 이전 페이지", ConsoleColor.Cyan, () => Shop.PageNumber--));
            SelectMenus.Add(new Menu("💵 아이템 판매", ConsoleColor.Cyan, () => MenuType = ShopMenuType.SELL));
            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));

            for (int i = 0; i < Shop.PageSlotCount; i++)
            {
                ShopMenu shopMenu = new(Shop, null);
                shopMenu.MyScene = this;
                ItemMenus.Add(shopMenu);
            }
        }

        public override void Init()
        {
            UpdateItemMenu();

            switch (MenuType)
            {
                case ShopMenuType.BUY:
                    Name = Shop.Name + " - 아이템 구매";
                    Description = "아이템을 구매할 수 있는 화면입니다.";
                    break;
                case ShopMenuType.SELL:
                    Name = Shop.Name + " - 아이템 판매";
                    Description = "아이템을 판매할 수 있는 화면입니다.";
                    break;
            }
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
            if(MenuType == ShopMenuType.BUY)
            {
                Console.WriteLine("───── 구매할 장비 ─────");
                ItemMenuDisplayMethod();
            }
            else if (MenuType == ShopMenuType.SELL)
            {
                Console.WriteLine("───── 판매할 장비 ─────");
                ItemMenuDisplayMethod();
            }

            GameManager.DisplayLine();
            GameManager.ColorWriteLine($"현재 페이지: {Shop.PageNumber + 1}/{Shop.PageCount}", ConsoleColor.Yellow); // 현재 페이지 출력
        }

        void UpdateItemMenu()
        {
            if(MenuType == ShopMenuType.BUY)
            {
                Shop.ShowItemsUpdate();

                if (Shop.ShowItems == null || Shop.ShowItems.Count == 0)
                    return;

                int index = 0;
                foreach (var menu in ItemMenus)
                {
                    ShopMenu shopMenu = menu as ShopMenu;
                    if (shopMenu == null) continue;
                    shopMenu.Item = null;

                    if (index < Shop.ShowItems.Count)
                    {
                        shopMenu.Item = Shop.ShowItems[index];
                        shopMenu.OnSelect = () => shopMenu.Buy(); // 구매 메소드 설정
                        index++;
                    }
                }
            }
            else if (MenuType == ShopMenuType.SELL)
            {
                int index = 0;
                foreach (var menu in ItemMenus)
                {
                    ShopMenu shopMenu = menu as ShopMenu;
                    if (shopMenu == null) continue;
                    shopMenu.Item = null;

                    if (index < GameManager.Player.Inventory.Count)
                    {
                        shopMenu.Item = GameManager.Player.Inventory[index];
                        shopMenu.OnSelect = () => shopMenu.Sell(); // 구매 메소드 설정
                        index++;
                    }
                }

            }


        }

    }
}
