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
            Description = $"500 G 를 내면 체력을 회복할 수 있습니다.(보유 골드 : {GameManager.Player.Gold} G) ";

            SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
            SelectMenus.Add(new Menu("💤 휴식하기", ConsoleColor.Yellow, () => GameManager.Instance.Rest()));
        }

        public override void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            GameManager.ColorWriteLine("💤 휴식", nameColor);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("휴식을 통해 체력을 회복할 수 있습니다.");
            Console.WriteLine($"500 G 를 지불하고 체력을 회복하세요. {Description}");
            Console.WriteLine();
        }

        public override void MainDisplay()
        {
            Console.WriteLine("[현재 체력]");
            GameManager.ColorWriteLine($"{GameManager.Player.HP} / {GameManager.Player.MaxHP}", ConsoleColor.Yellow);
            Console.WriteLine();
        }
    }
}
