//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Text;
//using System.Threading.Tasks;
//using TextRPG.Manager;

//namespace TextRPG
//{
//    internal class ToggShopItemMenu : ToggleItemMenu
//    {
//        public ToggShopItemMenu(Item item, ConsoleColor color, Action onSelect = null)
//        {
//            Item = item; // 아이템 설정
//            Content = GetBuyMenuItemInfo(item); // 메뉴 내용 설정
//            Color = color; // 메뉴 색상 설정
//            OnSelect = onSelect; // 메뉴 선택 시 호출될 액션 설정
//            isSelected = false; // 초기 선택 상태는 false

//            if (OnSelect == null)
//                OnSelect = () => ToggleItem(); // 기본 액션은 아이템 토글로 설정
//        }



//        public override void Display()
//        {
//            if (isSelected)
//            {
//                Console.BackgroundColor = ConsoleColor.DarkGray;
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            else
//            {
//                if (Item != null && GameManager.Player.HasEquippedItem(Item))
//                {
//                    Console.ForegroundColor = ConsoleColor.Yellow; // 이미 장착된 아이템은 노란색으로 표시
//                }
//                else
//                {
//                    Console.BackgroundColor = ConsoleColor.Black; // 선택되지 않은 메뉴는 검은색 배경으로 표시
//                    Console.ForegroundColor = Color;
//                }
//            }

//            // 메뉴 내용 출력
//            string content = isSelected ? $"▶   {Content}" : Content;
//            Console.WriteLine(content); // 메뉴 내용 출력
//            Console.ResetColor(); // 색상 초기화
//        }


//        public void ToggleItem()
//        {
//            if (Item == null) return;

//            if (GameManager.Player.HasEquippedItem(Item))
//            {
//                // 아이템이 이미 장착되어 있다면 해제
//                GameManager.Player.UnequipItem(Item);
//                GameManager.DisplayMessage($"{Item.Name} 아이템을 해제했습니다.");
//            }
//            else
//            {
//                GameManager.Player.EquipItem(Item);
//                GameManager.DisplayMessage($"{Item.Name} 아이템을 장착했습니다.");
//            }
//        }

//        public static string GetBuyMenuItemInfo(Item item)
//        {
//            if (item == null) return "               ";

//            return $"{item.Name} | {Item.GetItemTypeEffectText(item.Type)} +{item.EffectValue} | {item.Description}";
//        }

//    }
//}
