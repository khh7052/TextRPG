using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scene
{
    internal abstract class SceneBase
    {
        public string Name { get; set; } // 씬 이름
        public virtual string Description { get; set; } // 씬 설명

        public ConsoleColor NameColor = ConsoleColor.DarkYellow; // 씬 이름 색상
        public ConsoleColor DescriptionColor = ConsoleColor.White; // 씬 설명 색상

        private List<Menu> _menus = new List<Menu>(); // 씬에서 사용할 메뉴 리스트
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
            Init();
            InfoDisplay();
            // MainDisplay();
            MenuDisplay();
            // SelectDisplay();
            End();
        }

        // 씬 초기화 메서드
        public virtual void Init() { }

        // 씬 정보 출력 메서드
        public virtual void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.ForegroundColor = nameColor;
            Console.WriteLine(Name);
            Console.ForegroundColor = descriptionColor;
            Console.WriteLine(Description);
            Console.ResetColor();
        }

        // 메인 화면 출력
        public abstract void MainDisplay();

        public void MenuDisplay()
        {
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            foreach (var menu in _menus)
            {
                menu.Display();
            }
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }

        // 선택 UI 출력
        public virtual void SelectDisplay()
        {
            Console.Write("원하시는 행동을 선택해주세요: ");
        }

        public abstract void SelectMenu(int selection);

        // 씬 종료 메서드
        public virtual void End() { }
    }
}
