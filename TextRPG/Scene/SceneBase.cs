using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{
    internal abstract class SceneBase
    {
        public string Name { get; set; } // 씬 이름
        public virtual string Description { get; set; } // 씬 설명

        public ConsoleColor NameColor = ConsoleColor.DarkYellow; // 씬 이름 색상
        public ConsoleColor DescriptionColor = ConsoleColor.White; // 씬 설명 색상

        // private List<Menu> _itemMenus = new(); // 아이템 메뉴 리스트
        // private List<Menu> _selectMenus = new(); // 선택 메뉴 리스트

        public List<Menu> ItemMenus { get; set; } = new(); // 아이템 메뉴 리스트
        public List<Menu> SelectMenus { get; set; } = new();


        /*
        public List<Menu> Menus
        {
            get
            {
                return _menus;
            }
            set
            {
                _menus = value;
            }
        }
        */

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

        // 씬 시작 메서드
        public virtual void Start()
        {
            Console.ResetColor();
            Init();
            InfoDisplay();
            MainDisplay();
            SelectMenuDisplay();
            End();
        }

        // 씬 초기화 메서드
        public virtual void Init() { }

        // 씬 정보 출력 메서드
        public virtual void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            GameManager.ColorWriteLine(Name, nameColor);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine(Description);
            Console.WriteLine();
            /*
            Console.Clear();
            Console.ForegroundColor = nameColor;
            Console.WriteLine(Name);
            Console.ForegroundColor = descriptionColor;
            Console.WriteLine(Description);
            Console.ResetColor();
            */
        }

        // 메인 화면 출력
        public virtual void MainDisplay() { }


        public virtual void ItemMenuDisplay()
        {
            ItemMenuDisplayMethod();

            if(ItemMenus.Count == 0) return;
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
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
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
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
