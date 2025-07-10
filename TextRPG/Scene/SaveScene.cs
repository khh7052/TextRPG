using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;
using static TextRPG.Scene.ShopScene;

namespace TextRPG.Scene
{
    internal class SaveScene : SceneBase
    {
        public enum SaveMenu
        {
            SAVE,
            LOAD,
            DELETE
        }
        public SaveMenu Menu { get; set; } = SaveMenu.SAVE; // 기본값은 로비로 설정

        public SaveScene()
        {
            SelectMenus.Add(new Menu("💾 저장", ConsoleColor.Cyan, () => Menu = SaveMenu.SAVE));
            SelectMenus.Add(new Menu("📂 불러오기", ConsoleColor.Cyan, () => Menu = SaveMenu.LOAD));
            SelectMenus.Add(new Menu("🗑️ 삭제", ConsoleColor.Cyan, () => Menu = SaveMenu.DELETE));
            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));

            for (int i = 0; i < SaveManager.Instance.SaveFileLength; i++)
            {
                ItemMenus.Add(new FileMenu(SaveManager.Instance.SaveFiles[i]));
            }
        }

        public override void Init()
        {
            switch (Menu)
            {
                case SaveMenu.SAVE:
                    Name = "저장 화면 - 저장";
                    Description = "게임을 저장할 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        FileMenu fileMenu = ItemMenus[i] as FileMenu;
                        if (fileMenu != null)
                        {
                            fileMenu.OnSelect = () => fileMenu.Save(); // 저장 메소드 설정
                        }
                    }
                    break;
                case SaveMenu.LOAD:
                    Name = "저장 화면 - 불러오기";
                    Description = "저장된 게임을 불러올 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        FileMenu fileMenu = ItemMenus[i] as FileMenu;
                        if (fileMenu != null)
                        {
                            fileMenu.OnSelect = () => fileMenu.Load(); // 불러오기 메소드 설정
                        }
                    }
                    break;
                case SaveMenu.DELETE:
                    Name = "저장 화면 - 삭제";
                    Description = "저장된 게임을 삭제할 수 있는 화면입니다.";

                    for (int i = 0; i < ItemMenus.Count; i++)
                    {
                        FileMenu fileMenu = ItemMenus[i] as FileMenu;
                        if (fileMenu != null)
                        {
                            fileMenu.OnSelect = () => fileMenu.Delete(); // 삭제 메소드 설정
                        }
                    }
                    break;
            }
        }

        public override void Start()
        {
            Menu = SaveMenu.SAVE;
        }

        public override void MainDisplay()
        {
            Console.WriteLine("[파일 목록]");
            
            ItemMenuDisplayMethod();
            GameManager.DisplayLine();
        }
    }
}
