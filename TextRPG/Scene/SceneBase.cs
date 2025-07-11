using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;

namespace TextRPG.Scene
{
    internal abstract class SceneBase
    {
        public string Name { get; set; } // 씬 이름
        public virtual string Description { get; set; } // 씬 설명

        public ConsoleColor NameColor = ConsoleColor.DarkYellow; // 씬 이름 색상
        public ConsoleColor DescriptionColor = ConsoleColor.White; // 씬 설명 색상

        public List<Menu> ItemMenus { get; set; } = new(); // 아이템 메뉴 리스트
        public List<Menu> SelectMenus { get; set; } = new();

        public int IntroSpaceCount { get; set; } = 1; // 씬 시작 시 공백 줄 수

        private int _menuIndex; // 현재 선택된 메뉴 인덱스
        public int MenuTotalCount
        {
            get { return ItemMenus.Count + SelectMenus.Count; } // 전체 메뉴 개수
        }

        public int MenuIndex
        {
            get { return _menuIndex; }
            set
            {
                _menuIndex = value;

                // 인덱스가 범위를 벗어나지 않도록 조정
                if (_menuIndex < 0)
                {
                    _menuIndex = MenuTotalCount - 1; // 음수 인덱스는 마지막 메뉴로 설정
                }
                else if (_menuIndex >= MenuTotalCount)
                {
                    _menuIndex = 0; // 범위를 초과하면 처음으로 설정
                }
            }
        }

        public Menu CurrentMenu
        {
            get
            {
                // 0 ~ ItemMenus.Count - 1: 아이템 메뉴
                if (0 <= MenuIndex && MenuIndex < ItemMenus.Count)
                {
                    return ItemMenus[MenuIndex];
                }
                // ItemMenus.Count ~ SelectMenus.Count - 1: 선택 메뉴
                else if (ItemMenus.Count <= MenuIndex && MenuIndex < ItemMenus.Count + SelectMenus.Count)
                {
                    return SelectMenus[MenuIndex - ItemMenus.Count]; // 현재 선택된 아이템 메뉴 반환
                }
                else if (SelectMenus.Count <= MenuIndex)
                {
                    if (SelectMenus.Count > 0)
                        return SelectMenus[0]; // 현재 선택된 선택 메뉴 반환
                }

                return null;
            }
        }

        public SceneBase() { 
            Name = "기본 씬";
            Description = "기본 씬 설명입니다.";
        }

        public SceneBase(string name, string description, ConsoleColor nameColor, ConsoleColor descriptionColor)
        {
            Name = name;
            Description = description;
            NameColor = nameColor;
            DescriptionColor = descriptionColor;
        }


        // 씬 초기화 메서드
        public virtual void Init() { }

        // 씬 시작 전 초기화 메서드
        public virtual void Start() {
            CurrentMenu.IsSelected = true;
        }

        // 씬 시작 메서드
        public virtual void Update()
        {
            Console.ResetColor();
            Init();
            InfoDisplay();
            MainDisplay();
            SelectMenuDisplay();
            End();
        }

        // 씬 정보 출력 메서드
        public virtual void InfoDisplay()
        {
            Console.Clear();
            GameManager.DisplayLine();
            GameManager.ColorWriteLine(Name, NameColor);
            GameManager.DisplayLine(); 
            GameManager.ColorWriteLine(Description, DescriptionColor);
            GameManager.DisplayEnter(IntroSpaceCount);
        }

        // 메인 화면 출력
        public virtual void MainDisplay() { }
        public virtual void ItemMenuDisplay()
        {
            ItemMenuDisplayMethod();

            if(ItemMenus.Count == 0) return;
            GameManager.DisplayLine();
        }

        protected void ItemMenuDisplayMethod()
        {
            foreach (var menu in ItemMenus)
            {
                if (!menu.Enable) continue;
                menu.Display();
            }
        }

        public virtual void SelectMenuDisplay()
        {
            // Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            SelectMenuDisplayMethod();
            GameManager.DisplayLine();
        }

        // 선택 메뉴 처리 메서드
        protected void SelectMenuDisplayMethod()
        {
            foreach (var menu in SelectMenus)
            {
                if (!menu.Enable) continue;
                menu.Display();
            }
        }

        public virtual void SelectMenu(int selection) { }

        // 씬 종료 메서드
        public virtual void End() { }
    }
}
