using System.Xml.Linq;
using TextRPG.Manager;
using TextRPG.MenuCollections;
using TextRPG.Scene;

internal class GameOverScene : SceneBase
{
    public GameOverScene()
    {
        Name = "⚰ 묘지";
        NameColor = ConsoleColor.DarkRed;
        DescriptionColor = ConsoleColor.Gray;

        Description = """
            ☠☠☠━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━☠☠☠

                ⚰ 격렬한 전투 끝에 당신은 쓰러졌습니다...
                누군가가 당신의 시신을 이곳, 잊혀진 묘지에 묻었습니다.
                
                🕯️ 싸늘한 바람이 불고... 어둠이 당신을 감쌉니다.
                당신의 영혼은 어디로 향할까요?

            ☠☠☠━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━☠☠☠
            """;

        SelectMenus.Add(new Menu("👻 새로운 육신에 들어간다 (다시 시작)", ConsoleColor.Cyan, GameManager.Restart));
        SelectMenus.Add(new Menu("🔚 심연으로 돌아간다 (게임 종료)", ConsoleColor.DarkGray, GameManager.ExitGame));
    }
}