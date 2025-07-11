using System.Xml.Linq;
using TextRPG.Manager;
using TextRPG.MenuCollections;
using TextRPG.Scene;

internal class RestScene : SceneBase
{
    public override string Description
    {
        get => $"""
                ╔══════════════════════════════════════════╗
                  💤 휴식을 통해 체력을 회복할 수 있습니다.
                  500 G 를 지불하면 체력이 +100 회복됩니다.
                  현재 보유 골드 : {GameManager.Player.Gold} G
                ╚══════════════════════════════════════════╝
                """;
        set => base.Description = value;
    }

    public RestScene()
    {
        Name = "🛏️ 𝕽𝖊𝖘𝖙 𝕬𝖗𝖊𝖆 - 휴식소";
        NameColor = ConsoleColor.Yellow;
        DescriptionColor = ConsoleColor.Gray;

        SelectMenus.Add(new Menu("🌙 휴식하기 (500 G)", ConsoleColor.Yellow, () => GameManager.Instance.Rest()));
        SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));
    }

    public override void MainDisplay()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n✦━━━━━━━━━━━━━━✦ 현재 체력 ✦━━━━━━━━━━━━━━✦\n");
        Console.ResetColor();

        float hp = GameManager.Player.HP;
        float maxHp = GameManager.Player.MaxHP;

        GameManager.DrawBar("HP : ", hp, maxHp, ConsoleColor.Red);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n✦━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━✦\n");
        Console.ResetColor();
    }
}
