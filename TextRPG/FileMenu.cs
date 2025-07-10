using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class FileMenu : Menu
    {
        public FileMenu(string content = "", ConsoleColor color = ConsoleColor.White, Action onSelect = null) : base(content, color, onSelect)
        {
            // 파일 메뉴는 기본적으로 선택되지 않은 상태로 초기화
            isSelected = false;
        }

        public override void Display()
        {
            // 파일 메뉴는 선택된 상태로 표시
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            // 메뉴 내용 출력
            string content = $"📂 {Content}";
            Console.WriteLine(content);
            Console.ResetColor(); // 색상 초기화
        }

        public override void Select()
        {
            OnSelect?.Invoke(); // 파일 메뉴 선택 시 등록된 액션 호출
        }
    }
}
