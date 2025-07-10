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

            SelectMenus.Add(new("ℹ️ 상태 보기", ConsoleColor.Cyan, ()=> SceneManager.ChangeScene(SceneType.STATUS)));
            SelectMenus.Add(new("🎒 인벤토리", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.INVENTORY)));
            SelectMenus.Add(new("🛒 상점", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.SHOP)));
            SelectMenus.Add(new("🗡️ 던전 입장", ConsoleColor.Red, () => SceneManager.ChangeScene(SceneType.DUNGEON)));
            SelectMenus.Add(new("💤 휴식하기", ConsoleColor.Yellow, () => SceneManager.ChangeScene(SceneType.REST)));
            SelectMenus.Add(new("💾 저장하기 & 불러오기", ConsoleColor.Yellow, () => SceneManager.ChangeScene(SceneType.SAVE)));
            SelectMenus.Add(new("❌ 나가기", ConsoleColor.DarkBlue, () => GameManager.ExitGame()));
        }

        public override void InfoDisplay(ConsoleColor nameColor = ConsoleColor.DarkYellow, ConsoleColor descriptionColor = ConsoleColor.White)
        {
            Console.Clear();
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            GameManager.ColorWriteLine("⚔️  던전 입구", nameColor);
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("고대 유적의 문 앞에 서 있습니다.");
            Console.WriteLine("던전에 들어가기 전 장비와 물자를 점검하세요.");
            Console.WriteLine();
            Console.WriteLine(); 
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
