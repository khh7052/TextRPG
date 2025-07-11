using TextRPG.Manager;
using TextRPG.MenuCollections;
using TextRPG.Scene;

internal class StatusScene : SceneBase
{
    public StatusScene()
    {
        Name = "📜 𝕊𝕥𝕒𝕥𝕦𝕤 ─ 상태 보기";
        NameColor = ConsoleColor.Yellow;
        DescriptionColor = ConsoleColor.Gray;

        Description = """
            ╔═══════════════════════════════════════════════╗
              던전에 들어가기 전, 당신의 상태를 점검하세요.
              현재 능력치는 살아남기 위한 유일한 무기입니다.
            ╚═══════════════════════════════════════════════╝
            """;

        SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
    }

    public override void MainDisplay()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n✦━━━━━━━━━━━━━━━✦ 플레이어 상태 ✦━━━━━━━━━━━━━━━✦\n");

        Console.ResetColor();
        GameManager.Player.DisplayStatus(); // 캐릭터 상태 출력

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n✦━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━✦\n");
        Console.ResetColor();
    }
}
