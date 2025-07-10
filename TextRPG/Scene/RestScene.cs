using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{
    internal class RestScene : SceneBase
    {
        public override string Description { get => $"500 G 를 내면 체력을 회복할 수 있습니다.(보유 골드 : {GameManager.Player.Gold} G) "; set => base.Description = value; }

        public RestScene()
        {
            Name = "휴식";
        }

        public override void MainDisplay()
        {
            Console.WriteLine("[현재 체력]");
            GameManager.ColorWriteLine($"{GameManager.Player.HP} / {GameManager.Player.MaxHP}", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
        }

        public override void SelectMenu(int selection)
        {
            switch (selection)
            {
                case 1:
                    // 휴식 로직
                    GameManager.Instance.Rest();
                    break;
                case 0:
                    // 시작 씬으로 돌아가기
                    SceneManager.Instance.ChangeScene(SceneType.START);
                    break;
                default:
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    break;
            }
        }
    }
}
