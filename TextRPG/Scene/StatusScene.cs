using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{
    internal class StatusScene : SceneBase
    {
        public StatusScene()
        {
            Name = "상태 보기";
            Description = "현재 캐릭터의 상태를 확인할 수 있습니다.";
        }

        public override void MainDisplay()
        {
            GameManager.Player.DisplayStatus();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public override void SelectMenu(int selection)
        {
            switch (selection)
            {
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
