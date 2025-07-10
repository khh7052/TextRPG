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

        public override void Start()
        {
            InitItemMenu(); // 아이템 메뉴 업데이트
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
            Console.WriteLine("───── 보유 장비 ─────");
            ItemMenuDisplayMethod();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }


        void InitItemMenu()
        {
            _showItems = ItemManager.Instance.GetItemRange(GameManager.Player.Inventory, 0, 10); // 예시로 10개 아이템만 가져옴

            if (_showItems == null || _showItems.Count == 0)
                return;

            for (int i = 0; i < _showItems.Count; i++)
            {
                var item = _showItems[i];

                ToggleItemMenu toggleItemMenu = ItemMenus[i] as ToggleItemMenu;
                if (toggleItemMenu != null)
                {
                    toggleItemMenu.Item = item; // 아이템 설정
                }
            }
        }

        string GetEquipMenuItemInfo(Item item)
        {
            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description}";
        }

    }
}
