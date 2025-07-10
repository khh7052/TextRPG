using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;

namespace TextRPG
{
    internal class MenuController
    {
        public List<Menu> ItemMenus
        {
            get { return SceneManager.CurrentScene.ItemMenus; }
        }

        public List<Menu> SelectMenus
        {
            get { return SceneManager.CurrentScene.SelectMenus; }
        }

        private int _currentIndex; // 현재 선택된 메뉴 인덱스

        public int MenuTotalCount
        {
            get { return ItemMenus.Count + SelectMenus.Count; } // 전체 메뉴 개수
        }

        public int CurrentIdx
        {
            get { return _currentIndex; }
            set
            {
                _currentIndex = value;

                // 인덱스가 범위를 벗어나지 않도록 조정
                if (_currentIndex < 0)
                {
                    _currentIndex = MenuTotalCount - 1; // 음수 인덱스는 마지막 메뉴로 설정
                }
                else if (_currentIndex >= MenuTotalCount)
                {
                    _currentIndex = 0; // 범위를 초과하면 처음으로 설정
                }
            }
        }

        public Menu CurrentMenu
        {
            get
            {
                // 0 ~ ItemMenus.Count - 1: 아이템 메뉴
                if (0 <= CurrentIdx && CurrentIdx < ItemMenus.Count)
                {
                    return ItemMenus[CurrentIdx]; 
                }
                // ItemMenus.Count ~ SelectMenus.Count - 1: 선택 메뉴
                else if(ItemMenus.Count <= CurrentIdx && CurrentIdx < ItemMenus.Count + SelectMenus.Count)
                {
                    return SelectMenus[CurrentIdx - ItemMenus.Count]; // 현재 선택된 아이템 메뉴 반환
                }
                else if (SelectMenus.Count <= CurrentIdx)
                {
                    if(SelectMenus.Count > 0)
                        return SelectMenus[0]; // 현재 선택된 선택 메뉴 반환
                }

                return null;
            }
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
            if (MenuTotalCount == 0) return; // 메뉴가 없으면 아무 동작도 하지 않음
            CurrentMenu.isSelected = false;
            while (--CurrentIdx >= 0 && !CurrentMenu.Enable) ;
            CurrentMenu.isSelected = true;
        }

        public void MoveDown()
        {
            if (MenuTotalCount == 0) return; // 메뉴가 없으면 아무 동작도 하지 않음
            CurrentMenu.isSelected = false;
            while (++CurrentIdx < MenuTotalCount && !CurrentMenu.Enable) ;
            CurrentMenu.isSelected = true;
        }


        public void SelectMenu()
        {
            for (int i = 0; i < ItemMenus.Count; i++)
            {
                ItemMenus[i].isSelected = false; // 모든 아이템 메뉴 선택 해제
            }

            for (int i = 0; i < SelectMenus.Count; i++)
            {
                SelectMenus[i].isSelected = false; // 모든 선택 메뉴 선택 해제
            }
            CurrentMenu.Select(); // 현재 선택된 메뉴의 액션 실행
            CurrentIdx = 0;
        }

    }
}
