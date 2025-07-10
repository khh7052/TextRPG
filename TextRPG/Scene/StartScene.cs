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
        }

        public override void MainDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            GameManager.ColorWriteLine("4. 던전입장", ConsoleColor.Red);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 저장하기 & 불러오기");
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
