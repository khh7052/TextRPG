using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG.Scene
{

    internal class InventoryScene : SceneBase
    {
        public InventoryScene()
        {
            Name = "인벤토리";
            Description = "인벤토리를 확인하고 아이템을 관리할 수 있습니다.";
        }


        public override void MainDisplay()
        {
            Console.WriteLine("1. 장착 관리");
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
                case 1:
                    // 인벤토리 장비 씬으로 전환
                    SceneManager.Instance.ChangeScene(SceneType.INVENTORY_EQUIP);
                    break;
                default:
                    GameManager.DisplayWarning("잘못된 입력입니다. 주어진 선택지를 입력해주세요.");
                    break;
            }
        }
    }
}
