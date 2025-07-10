using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Menu
    {
        public Action OnSelect; // 메뉴 선택 시 호출되는 액션

        public string Content { get; set; } // 메뉴 내용
        public bool isSelected { get; set; } // 메뉴 선택 여부

        public Menu(string content, Action onSelect = null)
        {
            Content = content; // 메뉴 내용 설정
            OnSelect = onSelect; // 메뉴 선택 시 호출될 액션 설정
            isSelected = false; // 초기 선택 상태는 false
        }

        public void Display()
        {
            if (isSelected)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue; // 선택된 메뉴는 어두운 파란색 배경으로 표시
                Console.ForegroundColor = ConsoleColor.Yellow; // 선택된 메뉴는 노란색으로 표시
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black; // 선택되지 않은 메뉴는 검은색 배경으로 표시
                Console.ForegroundColor = ConsoleColor.White; // 선택되지 않은 메뉴는 흰색으로 표시
            }

            Console.WriteLine(Content); // 메뉴 내용 출력
            Console.ResetColor(); // 색상 초기화
        }

        public void Select()
        {
            OnSelect?.Invoke(); // 메뉴 선택 시 등록된 액션 호출
        }
    }
}
