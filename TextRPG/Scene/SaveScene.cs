using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using static TextRPG.Scene.ShopScene;

namespace TextRPG.Scene
{
    internal class SaveScene : SceneBase
    {
        public enum SaveMenu
        {
            LOBBY,
            SAVE,
            LOAD,
            DELETE
        }
        public SaveMenu Menu { get; set; } = SaveMenu.LOBBY; // 기본값은 로비로 설정

        public SaveScene()
        {
            Name = "저장 화면";
            Description = "게임을 저장하거나 불러올 수 있습니다.";

            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
            SelectMenus.Add(new Menu("💾 저장", ConsoleColor.Cyan, () => Menu = SaveMenu.SAVE));
            SelectMenus.Add(new Menu("📂 불러오기", ConsoleColor.Cyan, () => Menu = SaveMenu.LOAD));
            SelectMenus.Add(new Menu("🗑️ 삭제", ConsoleColor.Cyan, () => Menu = SaveMenu.DELETE));

            for (int i = 0; i < SaveManager.Instance.SaveFileLength; i++)
            {
                ItemMenus.Add(new FileMenu());
            }
        }

        public override void Init()
        {
            switch (Menu)
            {
                case SaveMenu.LOBBY:
                    Name = "저장 화면";
                    Description = "게임을 저장하거나 불러올 수 있습니다.";
                    break;
                case SaveMenu.SAVE:
                    Name = "저장 화면 - 저장";
                    Description = "게임을 저장할 수 있는 화면입니다.";
                    break;
                case SaveMenu.LOAD:
                    Name = "저장 화면 - 불러오기";
                    Description = "저장된 게임을 불러올 수 있는 화면입니다.";
                    break;
                case SaveMenu.DELETE:
                    Name = "저장 화면 - 삭제";
                    Description = "저장된 게임을 삭제할 수 있는 화면입니다.";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public override void MainDisplay()
        {
            switch (Menu)
            {
                case SaveMenu.LOBBY:
                    MainDisplay_Lobby();
                    break;

                case SaveMenu.SAVE:
                case SaveMenu.LOAD:
                case SaveMenu.DELETE:
                    MainDisplay_SLD();
                    break;
            }
        }

        public override void SelectMenu(int selection)
        {
            switch (Menu)
            {
                case SaveMenu.LOBBY:
                    SelectMenu_Lobby(selection);
                    break;

                case SaveMenu.SAVE:
                    SelectMenu_Save(selection);
                    break;

                case SaveMenu.LOAD:
                    SelectMenu_Load(selection);
                    break;
                case SaveMenu.DELETE:
                    SelectMenu_Delete(selection);
                    break;
            }
        }


        public void MainDisplay_Lobby()
        {
            Console.WriteLine("[파일 목록]");
            SaveManager.Instance.DisplaySaveFiles();

        }

        public void MainDisplay_SLD()
        {
            Console.WriteLine("[파일 목록]");
            SaveManager.Instance.DisplaySaveFiles();

            Console.WriteLine("========================================");
            Console.WriteLine("0. 돌아가기");
        }

        public void SelectMenu_Lobby(int selection)
        {
            switch (selection)
            {
                case 1:
                    Menu = SaveMenu.SAVE;
                    break;
                case 2:
                    Menu = SaveMenu.LOAD;
                    break;
                case 3:
                    Menu = SaveMenu.DELETE;
                    break;
                case 0:
                    SceneManager.ChangeScene(SceneType.START);
                    return; // 나가기 선택 시 씬 변경 후 종료
                default:
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    return; // 잘못된 입력 시 종료
            }
        }

        public void SelectMenu_Save(int selection)
        {
            if (selection == 0)
            {
                Menu = SaveMenu.LOBBY; // 돌아가기 선택 시 로비로 이동
            }
            else
            {
                int idx = selection - 1; // 선택된 인덱스 (1부터 시작하므로 -1)

                // 입력 확인
                if (idx < 0 || idx >= SaveManager.Instance.SaveFileLength)
                {
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    return;
                }

                SaveManager.Instance.Save(idx);
            }
        }

        public void SelectMenu_Load(int selection)
        {
            if (selection == 0)
            {
                Menu = SaveMenu.LOBBY; // 돌아가기 선택 시 로비로 이동
            }
            else
            {
                int idx = selection - 1; // 선택된 인덱스 (1부터 시작하므로 -1)

                // 입력 확인
                if (idx < 0 || idx >= SaveManager.Instance.SaveFileLength)
                {
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    return;
                }

                SaveManager.Instance.Load(idx);
            }
        }

        public void SelectMenu_Delete(int selection)
        {
            if (selection == 0)
            {
                Menu = SaveMenu.LOBBY; // 돌아가기 선택 시 로비로 이동
            }
            else
            {
                int idx = selection - 1; // 선택된 인덱스 (1부터 시작하므로 -1)

                // 입력 확인
                if (idx < 0 || idx >= SaveManager.Instance.SaveFileLength)
                {
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    return;
                }

                SaveManager.Instance.Delete(idx);
            }
        }
    }
}
