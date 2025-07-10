using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;
using TextRPG.MenuCollections;

namespace TextRPG.Scene
{
    internal class RestScene : SceneBase
    {
        public override string Description { get => $"휴식을 통해 체력을 회복할 수 있습니다.\n500 G 를 지불하고 체력을 회복하세요. (보유 골드 : {GameManager.Player.Gold} G) "; set => base.Description = value; }

        public RestScene()
        {
            Name = "💤 휴식";
            Description = $"휴식을 통해 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.Player.Gold} G) ";

            SelectMenus.Add(new Menu("💤 휴식하기", ConsoleColor.Yellow, () => GameManager.Instance.Rest()));
            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
        }

        public override void MainDisplay()
        {
            Console.WriteLine("[현재 체력]");
            GameManager.ColorWriteLine($"{GameManager.Player.HP} / {GameManager.Player.MaxHP}", ConsoleColor.Yellow);
            Console.WriteLine();
        }
    }
}
