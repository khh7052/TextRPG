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
            NameColor = ConsoleColor.DarkCyan;
            Name = "⚔️ 𝕋𝕒𝕝𝕖𝕤 𝕗𝕣𝕠𝕞 𝕥𝕙𝕖 𝔸𝕓𝕪𝕤𝕤 ⚔️ ";
            Description = """
                던전으로 들어가는 길은 이곳밖에 없다.
                준비를 맞치고 들어가자...

                메뉴 이동 : W,S or ⬆, ⬇
                메뉴 선택 : Enter
                """;

            SelectMenus.Add(new("ℹ️ 상태 보기", ConsoleColor.Cyan, ()=> SceneManager.ChangeScene(SceneType.STATUS)));
            SelectMenus.Add(new("🎒 인벤토리", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.INVENTORY)));
            SelectMenus.Add(new("💰 상점", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.SHOP)));
            SelectMenus.Add(new("🏰 던전 입장", ConsoleColor.Red, () => SceneManager.ChangeScene(SceneType.DUNGEON)));
            SelectMenus.Add(new("💤 휴식하기", ConsoleColor.Yellow, () => SceneManager.ChangeScene(SceneType.REST)));
            SelectMenus.Add(new("📘 저장하기 & 불러오기", ConsoleColor.Yellow, () => SceneManager.ChangeScene(SceneType.SAVE)));
            SelectMenus.Add(new("❌ 나가기", ConsoleColor.DarkBlue, () => GameManager.ExitGame()));

            IntroSpaceCount = 3; // 인트로 스페이스 카운트 설정
        }

    }
}
