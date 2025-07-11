﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.MenuCollections
{
    internal class InventoryMenu : Menu
    {
        public Item Item { get; set; }

        public InventoryMenu(Item item)
        {
            Item = item;
            OnSelect = ToggleItem;
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
                if (Item != null && GameManager.Player.HasEquippedItem(Item))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // 이미 장착된 아이템은 노란색으로 표시
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black; // 선택되지 않은 메뉴는 검은색 배경으로 표시

                    if (Item == null)
                        Console.ForegroundColor = Color; // 기본 색상 사용
                    else
                    {
                        if (Item.Type == ItemType.WEAPON)
                            Console.ForegroundColor = ConsoleColor.Cyan; // 무기는 청록색으로 표시
                        else if (Item.Type == ItemType.ARMOR)
                            Console.ForegroundColor = ConsoleColor.Green; // 방어구는 초록색으로 표시
                    }
                }
            }

            Content = GetItemInfo(Item);
            string content = IsSelected ? $"▶   {Content}" : Content;
            Console.WriteLine(content);
            Console.ResetColor(); // 색상 초기화
        }


        public void ToggleItem()
        {
            if (Item == null) return;

            if (GameManager.Player.HasEquippedItem(Item))
            {
                // 아이템이 이미 장착되어 있다면 해제
                GameManager.Player.UnequipItem(Item);
                GameManager.DisplayMessage($"{Item.Name} 아이템을 해제했습니다.");
            }
            else
            {
                GameManager.Player.EquipItem(Item);
                GameManager.DisplayMessage($"{Item.Name} 아이템을 장착했습니다.");
            }
        }

        public static string GetItemInfo(Item item)
        {
            if (item == null) return "                   ";

            string icon = Item.GetItemIcon(item.Type);
            string name = $"{icon} {item.Name}".PadRight(14);       // 이름 필드 (14칸)
            string effect = $"{Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue}".PadRight(14); // 효과 필드 (14칸)
            string description = item.Description.PadRight(14);
            string gold = $"{item.Price} Gold".PadRight(14); // 가격 필드 (14칸)
            
            bool hasEquippedItem = GameManager.Player.HasEquippedItem(item);
            string equippedStatus = hasEquippedItem ? "✅" : ""; // 장착 여부 표시

            return $"{equippedStatus} {name}| {effect}| {description}| {gold}";
        }
    }
}
