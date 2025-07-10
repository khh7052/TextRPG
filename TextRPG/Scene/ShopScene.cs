using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{
    internal class ShopScene : SceneBase
    {
        public enum ShopMenu
        {
            LOBBY = 0, // 로비
            BUY = 1, // 아이템 구매
            SELL = 2, // 아이템 판매
        }

        public Shop Shop { get; set; } // 상점 인스턴스 생성

        public ShopMenu Menu { get; set; } = ShopMenu.LOBBY;

        public ShopScene()
        {
            Shop = new("상점", "아이템을 구매하거나 판매할 수 있는 곳입니다.");
            Name = Shop.Name;
            Description = Shop.Description;

            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
            SelectMenus.Add(new Menu("🛒 아이템 구매", ConsoleColor.Cyan, () => Menu = ShopMenu.BUY));
            SelectMenus.Add(new Menu("💵 아이템 판매", ConsoleColor.Cyan, () => Menu = ShopMenu.SELL));

            for (int i = 0; i < 10; i++)
            {
                ItemMenus.Add(new ToggleItemMenu(null, ConsoleColor.Cyan, null));
            }
        }

        public override void Init()
        {
            switch (Menu)
            {
                case ShopMenu.LOBBY:
                    Name = Shop.Name;
                    Description = Shop.Description;
                    break;
                case ShopMenu.BUY:
                    Name = Shop.Name + " - 아이템 구매";
                    Description = "아이템을 구매할 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        ToggleItemMenu toggleItemMenu = ItemMenus[i] as ToggleItemMenu;
                        if (toggleItemMenu != null)
                        {
                            toggleItemMenu.OnSelect = () => BuyMenu(toggleItemMenu.Item); // 구매 메소드 설정
                        }
                    }
                    break;
                case ShopMenu.SELL:
                    Name = Shop.Name + " - 아이템 판매";
                    Description = "아이템을 판매할 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        ToggleItemMenu toggleItemMenu = ItemMenus[i] as ToggleItemMenu;
                        if (toggleItemMenu != null)
                        {
                            toggleItemMenu.OnSelect = () => SellMenu(toggleItemMenu.Item); // 판매 메소드 설정
                        }
                    }
                    break;
            }
        }

        public override void Start()
        {
            UpdateItemMenu();
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

        public void BuyMenu(Item item)
        {
            // 똑같은 아이템이 있으면 경고
            if (GameManager.Player.HasItem(item))
            {
                GameManager.DisplayWarning($"{item.Name} 아이템은 이미 보유하고 있습니다. 다른 아이템을 선택해주세요.");
                return;
            }

            // 구매 가능하면 구매
            if (Shop.BuyItem(GameManager.Player, item))
                GameManager.DisplayMessage($"{item.Name} 아이템을 구매했습니다.");
            // 구매 불가능하면 경고
            else
                GameManager.DisplayWarning("구매에 실패했습니다. 골드가 부족합니다.");
        }

        public void SellMenu(Item item)
        {
            // 구매 가능하면 구매
            if (Shop.SellItem(GameManager.Player, item))
                GameManager.DisplayMessage($"{item.Name} 아이템을 판매했습니다. + {Shop.GetSellGold(item)} G");
            // 구매 불가능하면 경고
            else
                GameManager.DisplayWarning("판매에 실패했습니다. 인벤토리내에 아이템이 존재하지 않습니다.");
        }
    }
}
