using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;

namespace TextRPG.Scene
{
    internal class StatusScene : SceneBase
    {
        public StatusScene()
        {
            Name = "ℹ️  상태 보기";
            Description = "던전에 들어가기 전 캐릭터의 상태를 점검하세요.";

            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
        }


        public override void MainDisplay()
        {
            // 플레이어 상태 출력 전에 구분선 추가
            Console.WriteLine("───── 플레이어 상태 ─────");
            GameManager.Player.DisplayStatus();
            Console.WriteLine("──────────────────────────");
            Console.WriteLine();
        }

    }
}
