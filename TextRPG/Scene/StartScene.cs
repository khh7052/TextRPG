
using TextRPG.Scene;
using TextRPG.Manager;

internal class StartScene : SceneBase
{
    public StartScene()
    {
        NameColor = ConsoleColor.DarkCyan;
        Name = "⚔️ 𝕋𝕒𝕝𝕖𝕤 𝕗𝕣𝕠𝕞 𝕥𝕙𝕖 𝔸𝕓𝕪𝕤𝕤 ⚔️";

        DescriptionColor = ConsoleColor.Gray;
        Description = """
            ╔══════════════════════════════════════════════╗
              심연으로 향하는 문은 단 하나.
              이곳에서 준비를 마친 자만이 들어갈 수 있다.
              
              📜 메뉴 이동 : W, S 또는 ⬆, ⬇
              ✅ 메뉴 선택 : Enter
            ╚══════════════════════════════════════════════╝
            """;

        SelectMenus.Add(new("🧍 상태 보기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.STATUS)));
        SelectMenus.Add(new("🎒 인벤토리", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.INVENTORY)));
        SelectMenus.Add(new("🛒 상점", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.SHOP)));
        SelectMenus.Add(new("🏰 던전 입장", ConsoleColor.Red, () => SceneManager.ChangeScene(SceneType.DUNGEON)));
        SelectMenus.Add(new("💤 휴식하기", ConsoleColor.Yellow, () => SceneManager.ChangeScene(SceneType.REST)));
        SelectMenus.Add(new("💾 저장 & 불러오기", ConsoleColor.Yellow, () => SceneManager.ChangeScene(SceneType.SAVE)));
        SelectMenus.Add(new("❌ 게임 종료", ConsoleColor.DarkRed, () => GameManager.ExitGame()));

        IntroSpaceCount = 3;
    }
}
