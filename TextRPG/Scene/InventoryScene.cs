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

        public enum InventoryMenuType
        {
            EQUIP, // 장비 관리
        }

        public InventoryMenuType MenuType { get; set; } = InventoryMenuType.EQUIP; // 현재 메뉴 상태

        public InventoryScene()
        {
            Name = "🎒 𝕀𝕟𝕧𝕖𝕟𝕥𝕠𝕣𝕪 - 장착 관리";
            NameColor = ConsoleColor.Yellow;
            DescriptionColor = ConsoleColor.Gray;

            Description = """
            ╔═══════════════════════════════════════════════╗
              당신이 획득한 장비들을 확인할 수 있습니다.
              장착 상태나 효과를 확인하고 전략을 세우세요.
            ╚═══════════════════════════════════════════════╝
            """;

            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
            for (int i = 0; i < 10; i++)
            {
                ItemMenus.Add(new InventoryMenu(null));
            }
        }

        public override void Start()
        {
            base.Start();
            InitItemMenu(); // 아이템 메뉴 업데이트
        }

        public override void MainDisplay()
        {
            ItemMenuDisplay();
        }


        public override void ItemMenuDisplay()
        {
            Console.WriteLine();
            GameManager.ColorWriteLine("📦━━━━━━━━━━━━━━━📦 보유 장비 📦━━━━━━━━━━━━━━━📦", ConsoleColor.DarkGray);
            ItemMenuDisplayMethod();
            GameManager.DisplayLine();
        }


        void InitItemMenu()
        {
            _showItems = ItemManager.Instance.GetItemRange(GameManager.Player.Inventory, 0, 10); // 예시로 10개 아이템만 가져옴

            if (_showItems == null || _showItems.Count == 0)
                return;

            for (int i = 0; i < _showItems.Count; i++)
            {
                var item = _showItems[i];

                InventoryMenu inventoryMenu = ItemMenus[i] as InventoryMenu;
                if (inventoryMenu != null)
                {
                    inventoryMenu.Item = item; // 아이템 설정
                }
            }
        }

        string GetEquipMenuItemInfo(Item item)
        {
            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description}";
        }

    }
}
