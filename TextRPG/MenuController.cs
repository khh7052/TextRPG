using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;
using TextRPG.Scene;

namespace TextRPG
{
    internal class MenuController
    {

        public List<Menu> ItemMenus
        {
            get
            {
                if (SceneManager.CurrentScene == null) return null; // 현재 씬이 없으면 빈 리스트 반환

                return SceneManager.CurrentScene.ItemMenus;
            }
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
            get { return SceneManager.CurrentScene.MenuIndex; }
            set
            {
                SceneManager.CurrentScene.MenuIndex = value;
            }
        }

        public Menu CurrentMenu
        {
            get
            {
                return SceneManager.CurrentScene.CurrentMenu;
            }
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
            CurrentMenu.IsSelected = false;
            while (--CurrentIdx >= 0 && !CurrentMenu.Enable);
            CurrentMenu.IsSelected = true;
        }

        public void MoveDown()
        {
            if (MenuTotalCount == 0) return; // 메뉴가 없으면 아무 동작도 하지 않음
            CurrentMenu.IsSelected = false;
            while (++CurrentIdx < MenuTotalCount && !CurrentMenu.Enable);
            CurrentMenu.IsSelected = true;
        }


        public void SelectMenu()
        {
            CurrentMenu.Select(); // 현재 선택된 메뉴의 액션 실행
        }

    }
}
