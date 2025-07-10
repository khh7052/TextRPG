using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{

    internal class InventoryScene : SceneBase
    {
        List<Item> _showItems;

        public enum InventoryMenu
        {
            LOBBY, // 로비
            EQUIP, // 장비 관리
        }

        public InventoryMenu Menu { get; set; } = InventoryMenu.LOBBY; // 현재 메뉴 상태

        public InventoryScene()
        {
            Name = "인벤토리";
            Description = "인벤토리를 확인하고 아이템을 관리할 수 있습니다.";

            // Menus.Add(new Menu("🛡️ 장착 관리", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.INVENTORY_EQUIP)));

            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
            for (int i = 0; i < 10; i++)
            {
                ItemMenus.Add(new ToggleItemMenu(null, ConsoleColor.Cyan));
            }
        }

        public override void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            GameManager.ColorWriteLine("🎒 인벤토리", nameColor);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("아이템을 확인하고 필요한 장비를 관리하세요.");
            Console.WriteLine();
        }

        public override void MainDisplay()
        {
            ItemMenuDisplay();
        }


        public override void ItemMenuDisplay()
        {
            UpdateItemMenu();

            Console.WriteLine("───── 보유 장비 ─────");
            ItemMenuDisplayMethod();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            // Menus[0].Display(); // 마지막 메뉴는 돌아가기
        }



        void UpdateItemMenu()
        {
            _showItems = ItemManager.Instance.GetItemRange(GameManager.Player.Inventory, 0, 10); // 예시로 10개 아이템만 가져옴

            if (_showItems == null || _showItems.Count == 0)
                return;

            for (int i = 1; i < ItemMenus.Count; i++)
            {
                var item = _showItems[i];
                ToggleItemMenu toggleItemMenu = ItemMenus[i] as ToggleItemMenu;

                if (toggleItemMenu != null)
                {
                    toggleItemMenu.Item = item; // 아이템 설정
                }
            }
        }


        void DisplayInventoryItemList()
        {
            if (_showItems == null || _showItems.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
                return;
            }

            for (int i = 0; i < _showItems.Count; i++)
            {
                var item = _showItems[i];
                Console.ForegroundColor = item.Type == ItemType.WEAPON ? ConsoleColor.Cyan : ConsoleColor.Green;
                string itemInfo = GetEquipMenuItemInfo(item);

                if (GameManager.Player.HasEquippedItem(item))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // 이미 장착된 아이템은 노란색으로 표시
                    Console.WriteLine($"[E] {i + 1}. {itemInfo}");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {itemInfo}");
                }
            }
            Console.ResetColor();
        }

        string GetEquipMenuItemInfo(Item item)
        {
            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description}";
        }

    }
}
