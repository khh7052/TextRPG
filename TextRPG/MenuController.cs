using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG
{
    internal class MenuController
    {
        public List<Menu> Menus
        {
            get { return SceneManager.CurrentScene.Menus; }
        }

        private int _currentIndex; // 현재 선택된 메뉴 인덱스

        public Menu CurrentMenu
        {
            get { return Menus[_currentIndex]; }
        }


        public MenuController()
        {
            _currentIndex = 0; // 초기 선택 인덱스는 0
        }

        public void Input()
        {
            ConsoleKey key = Console.ReadKey(true).Key; // 키 입력 대기

            if (key == ConsoleKey.UpArrow || key == ConsoleKey.W) // 위 화살표 키
            {
                MoveUp();
            }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S) // 아래 화살표 키
            {
                MoveDown();
            }
            else if (key == ConsoleKey.Enter) // Enter 키
            {
                SelectMenu();
            }
        }

        public void MoveUp()
        {
            if (Menus.Count == 0) return; // 메뉴가 없으면 아무 동작도 하지 않음
            CurrentMenu.isSelected = false;

            GameManager.DisplayMessage("위로 이동"); // 현재 선택된 메뉴 이름 출력

            _currentIndex--; // 위로 이동 (메뉴의 시작이 위에서부터 아래로 향할것이기 때문)
            if (_currentIndex < 0) // 인덱스가 0보다 작아지면 마지막 메뉴로 이동
                _currentIndex = Menus.Count - 1;

            CurrentMenu.isSelected = true;
        }

        public void MoveDown()
        {
            if (Menus.Count == 0) return; // 메뉴가 없으면 아무 동작도 하지 않음
            CurrentMenu.isSelected = false;
            GameManager.DisplayMessage("아래로 이동"); // 현재 선택된 메뉴 이름 출력

            _currentIndex++; // 아래로 이동
            if (_currentIndex >= Menus.Count) // 인덱스가 메뉴의 개수를 초과하면 처음으로 이동
                _currentIndex = 0;

            CurrentMenu.isSelected = true;
        }

        public void AddMenu(Menu menu)
        {
            Menus.Add(menu); // 메뉴 목록에 새 메뉴 추가
        }

        public void SelectMenu()
        {
            CurrentMenu.Select(); // 현재 선택된 메뉴의 액션 실행
        }

    }
}
