using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.MenuCollections
{
    internal class Menu
    {
        public Action OnSelect; // 메뉴 선택 시 호출되는 액션

        public string Content { get; set; } // 메뉴 내용
        private bool _isSelected; // 메뉴 선택 상태
        public bool IsSelected
        {
            get
            {
                return _isSelected; // 현재 메뉴가 선택되었는지 여부 반환
            }
            set
            {
                _isSelected = value; // 메뉴 선택 상태 설정
            }
        }
        public ConsoleColor Color { get; set; } = ConsoleColor.White; // 메뉴 출력 시 사용할 색상
        public bool Enable { get; set; } = true; // 메뉴 활성화 여부

        public Menu()
        {
            Content = "기본 메뉴"; // 기본 메뉴 내용 설정
            Color = ConsoleColor.White; // 기본 색상은 흰색
            OnSelect = null; // 기본 액션은 null
            IsSelected = false; // 초기 선택 상태는 false
        }


        public Menu(string content, ConsoleColor color, Action onSelect = null)
        {
            Content = content; // 메뉴 내용 설정
            Color = color; // 메뉴 색상 설정
            OnSelect = onSelect; // 메뉴 선택 시 호출될 액션 설정
            IsSelected = false; // 초기 선택 상태는 false
        }

        public virtual void Display()
        {
            if (!Enable) return;

            Console.BackgroundColor = IsSelected ? ConsoleColor.DarkGray : ConsoleColor.Black; // 선택된 메뉴는 어두운 회색 배경
            Console.ForegroundColor = IsSelected ? ConsoleColor.White : Color; // 메뉴 색상 설정

            // 메뉴 내용 출력
            string content = IsSelected ? $"▶   {Content}" : Content;
            Console.WriteLine(content); // 메뉴 내용 출력
            Console.ResetColor(); // 색상 초기화
        }

        public virtual void Select()
        {
            if (!Enable) return;

            OnSelect?.Invoke(); // 메뉴 선택 시 등록된 액션 호출
        }

    }
}
