using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{
    internal class InventoryEquipScene : SceneBase
    {
        List<Item> _showItems;

        public InventoryEquipScene()
        {
            Name = "인벤토리 - 장비";
            Description = "장비를 착용하거나 해제할 수 있습니다.";
        }

        public override void MainDisplay()
        {
            Console.WriteLine("[장비 목록]");
            DisplayInventoryItemList();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public override void SelectMenu(int selection)
        {

            switch (selection)
            {
                case 0:
                    SceneManager.ChangeScene(SceneType.INVENTORY);
                    break;
                default:
                    if (selection < 1 || selection > ItemManager.Instance.items.Count)
                    {
                        GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                        return;
                    }
                    var item = _showItems[selection - 1];
                    if (GameManager.Player.HasEquippedItem(item))
                    {
                        // 아이템이 이미 장착되어 있다면 해제
                        GameManager.Player.UnequipItem(item);
                        GameManager.DisplayMessage($"{item.Name} 아이템을 해제했습니다.");
                    }
                    else
                    {
                        GameManager.Player.EquipItem(item);
                        GameManager.DisplayMessage($"{item.Name} 아이템을 장착했습니다.");
                    }

                    break;
            }
        }
        void DisplayInventoryItemList()
        {
            _showItems = ItemManager.Instance.GetItemRange(GameManager.Player.Inventory, 0, 10); // 예시로 10개 아이템만 가져옴
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
