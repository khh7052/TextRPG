using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    enum ItemType
    {
        WEAPON, // 무기
        ARMOR, // 방어구
    }

    internal class Item
    {
        public string Name { get; set; } // 아이템 이름
        public string Description { get; set; } // 아이템 설명
        public int Price { get; set; } // 아이템 가격
        public int EffectValue { get; set; } // 아이템 효과 값 (예: 회복량, 공격력 증가 등)
        public ItemType Type { get; set; } // 아이템 타입

        public Item(string name, string description, int price, int effectValue, ItemType type)
        {
            Name = name;
            Description = description;
            Price = price;
            EffectValue = effectValue;
            Type = type;
        }

        // 복사 메서드
        public Item Clone()
        {
            return new Item(Name, Description, Price, EffectValue, Type);
        }


        public static string GetItemTypeText(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.WEAPON => "무기",
                ItemType.ARMOR => "방어구",
                _ => "❓"
            };
        }

        public static string GetItemTypeEffectText(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.WEAPON => "공격력",
                ItemType.ARMOR => "방어력",
                _ => "❓"
            };
        }

        public static string GetItemIcon(ItemType type)
        {
            return type switch
            {
                ItemType.WEAPON => "🗡️",
                ItemType.ARMOR => "🛡️",
                _ => "❓"
            };
        }


    }
}
