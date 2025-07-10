using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{
    internal class StartScene : SceneBase
    {
        public StartScene()
        {
            Name = "던전 입구";
            Description = "던전 입구에 무사히 오신것을 환영합니다.\n이곳에서 던전으로 들어가기전 재정비를 할 수 있습니다.";

            Menus.Add(new("1. 📊 상태 보기", ()=> SceneManager.Instance.ChangeScene(SceneType.STATUS)));
            Menus.Add(new("2. 🎒 인벤토리", () => SceneManager.Instance.ChangeScene(SceneType.INVENTORY)));
            Menus.Add(new("3. 🛒 상점", () => SceneManager.Instance.ChangeScene(SceneType.SHOP)));
            Menus.Add(new("4. 🗡️ 던전 입장", () => SceneManager.Instance.ChangeScene(SceneType.DUNGEON)));
            Menus.Add(new("5. 💤 휴식하기", () => SceneManager.Instance.ChangeScene(SceneType.REST)));
            Menus.Add(new("6. 💾 저장하기 & 불러오기", () => SceneManager.Instance.ChangeScene(SceneType.SAVE)));
            Menus.Add(new("0. ❌ 나가기", () => GameManager.ExitGame()));
        }

        public override void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("📍 던전 입구");
            Console.WriteLine("던전 입구에 무사히 오신 것을 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 재정비를 할 수 있습니다.");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.ResetColor();
        }

        public override void MainDisplay()
        {
            GameManager.ColorWriteLine("1. 📊 상태 보기", ConsoleColor.Cyan);
            GameManager.ColorWriteLine("2. 🎒 인벤토리", ConsoleColor.Cyan);
            GameManager.ColorWriteLine("3. 🛒 상점", ConsoleColor.Cyan);
            GameManager.ColorWriteLine("4. 🗡️ 던전 입장", ConsoleColor.Red);
            GameManager.ColorWriteLine("5. 💤 휴식하기", ConsoleColor.Yellow);
            GameManager.ColorWriteLine("6. 💾 저장하기 & 불러오기", ConsoleColor.Yellow);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.ResetColor();
        }

        public override void SelectMenu(int selection)
        {
            switch (selection)
            {
                case 1:
                    // 상태 보기 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.STATUS);
                    break;
                case 2:
                    // 인벤토리 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.INVENTORY);
                    break;
                case 3:
                    // 상점 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.SHOP);
                    break;
                case 4:
                    // 던전 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.DUNGEON);
                    break;
                case 5:
                    // 휴식 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.REST);
                    break;
                case 6:
                    // 저장 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.SAVE);
                    break;
                default:
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    break;
            }
        }
    }
}
