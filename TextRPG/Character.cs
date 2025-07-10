using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG
{
    public enum ClassType
    {
        전사,
        마법사,
        도적
    }

    internal class Character
    {
        public string Name { get; set; }
        public int LV { get; set; } // 레벨
        public float MaxHP { get; set; } // 최대 체력
        public float HP { get; set; } // 체력
        public float ATK { get; set; } // 공격력
        public float DEF { get; set; } // 방어력
        public int Gold { get; set; } // 골드

        public Item Weapon { get; set; } // 무기
        public Item Armor { get; set; } // 방어구

        public ClassType Class { get; set; } // 캐릭터 클래스

        public List<Item> Inventory { get; set; } // 인벤토리

        public int MaxExperience { get; set; } = 1; // 최대 경험치 (레벨업 기준)

        private int _experience; // 현재 경험치
        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                if (_experience >= MaxExperience)
                {
                    LevelUp(); // 경험치가 최대치에 도달하면 레벨업
                }
            }
        }


        public Character()
        {
            Name = "플레이어";
            LV = 1;
            MaxHP = 100; // 최대 체력
            HP = MaxHP;
            ATK = 10;
            DEF = 5;
            Gold = 1000;

            MaxExperience = 1; // 초기 최대 경험치 설정
            Experience = 0; // 초기 경험치 설정

            Weapon = null; // 초기 무기 없음
            Armor = null; // 초기 방어구 없음
            Inventory = new List<Item>(); // 인벤토리 초기화


            AddItem(ItemManager.Instance.items[0]); // 기본 아이템 추가
            AddItem(ItemManager.Instance.items[1]); // 기본 아이템 추가
            AddItem(ItemManager.Instance.items[2]); // 기본 아이템 추가
        }

        public Character(string name, ClassType classType)
        {
            Name = name;
            Class = classType;
            Weapon = null; // 초기 무기 없음
            Armor = null; // 초기 방어구 없음
            Inventory = new List<Item>(); // 인벤토리 초기화

            switch (classType)
            {
                case ClassType.전사:
                    LV = 1;
                    MaxHP = 120;
                    ATK = 15;
                    DEF = 10;
                    Gold = 50;
                    break;
                case ClassType.마법사:
                    LV = 1;
                    MaxHP = 80;
                    ATK = 20;
                    DEF = 5;
                    Gold = 30;
                    break;
                case ClassType.도적:
                    LV = 1;
                    MaxHP = 100;
                    ATK = 12;
                    DEF = 8;
                    Gold = 40;
                    break;
            }

            AddItem(ItemManager.Instance.items[0]); // 기본 아이템 추가

            HP = MaxHP; // 초기 체력 설정
        }

        public void LevelUp()
        {
            LV++;
            MaxHP += 10; // 레벨업 시 최대 체력 증가
            HP = MaxHP; // 레벨업 시 체력 회복
            ATK += 0.5f; // 레벨업 시 공격력 증가
            DEF += 1; // 레벨업 시 방어력 증가

            Experience = 0; // 레벨업 후 경험치 초기화
            MaxExperience++; // 다음 레벨업을 위한 최대 경험치 증가
        }

        // 아이템 흭득
        public void AddItem(Item item)
        {
            Inventory.Add(item); // 인벤토리에 아이템 추가
        }

        // 아이템 제거
        public void RemoveItem(Item item)
        {
            if (HasItem(item))
            {
                if(HasEquippedItem(item))
                    UnequipItem(item); // 아이템이 장착되어 있다면 해제

                Inventory.Remove(item); // 인벤토리에서 아이템 제거
            }
        }


        public void EquipItem(Item item)
        {
            if (item.Type == ItemType.WEAPON)
            {
                Weapon = item;
                ATK += item.EffectValue; // 무기 효과 적용
            }
            else if (item.Type == ItemType.ARMOR)
            {
                Armor = item;
                DEF += item.EffectValue; // 방어구 효과 적용
            }
        }

        public void UnequipItem(Item item)
        {
            if (item.Type == ItemType.WEAPON && Weapon == item)
            {
                ATK -= item.EffectValue; // 무기 효과 제거
                Weapon = null;
            }
            else if (item.Type == ItemType.ARMOR && Armor == item)
            {
                DEF -= item.EffectValue; // 방어구 효과 제거
                Armor = null;
            }
        }

        public bool HasEquippedItem(Item item)
        {
            if (item == null) return false;

            if (item.Type == ItemType.WEAPON)
            {
                return Weapon == item;
            }
            else if (item.Type == ItemType.ARMOR)
            {
                return Armor == item;
            }
            return false;
        }

        public bool HasItem(Item item)
        {
            return Inventory.Contains(item);
        }

        public void DisplayStatus()
        {
            string atkItemStatus = Weapon != null ? $"(+{Weapon.EffectValue})" : "";
            string defItemStatus = Armor != null ? $"(+{Armor.EffectValue})" : "";

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Yellow;
            // Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine($"⚔️ Lv. {LV}     📛 {Name} ({Class})");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"✨ 경험치 : {Experience}/{MaxExperience}  ({Math.Round((float)Experience / MaxExperience * 100, 2)}%)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🩸 체력     : {HP}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"🗡️ 공격력   : {ATK} {atkItemStatus}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"🛡️ 방어력   : {DEF} {defItemStatus}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"💰 골드     : {Gold} G");

            Console.ResetColor();
            Console.WriteLine("════════════════════════════════════════");

            Console.WriteLine("🎒 장비");
            Console.WriteLine($"   🗡️ 무기   : {(Weapon != null ? $"{Weapon.Name} (공격력 +{Weapon.EffectValue})" : "없음")}");
            Console.WriteLine($"   🛡️ 방어구 : {(Armor != null ? $"{Armor.Name} (방어력 +{Armor.EffectValue})" : "없음")}");
        }

    }
}
