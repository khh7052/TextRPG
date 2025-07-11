using System.Xml.Linq;
using TextRPG.Manager;
using TextRPG.MenuCollections;
using TextRPG.Scene;
using TextRPG;

internal class ShopScene : SceneBase
{
    public enum ShopMenuType
    {
        BUY = 1,
        SELL = 2,
    }

    public Shop Shop { get; set; }
    public ShopMenuType MenuType { get; set; } = ShopMenuType.BUY;

    public ShopScene()
    {
        Shop = new("💰 𝕊𝕙𝕠𝕡 𝕠𝕗 𝔸𝕓𝕪𝕤𝕤", "고대 유적에서 살아남은 자들만 출입할 수 있는 심연의 상점.");
        Name = Shop.Name;
        Description = Shop.Description;

        SelectMenus.Add(new Menu("▶ 다음 페이지", ConsoleColor.DarkCyan, () => Shop.PageNumber++));
        SelectMenus.Add(new Menu("◀ 이전 페이지", ConsoleColor.DarkCyan, () => Shop.PageNumber--));
        SelectMenus.Add(new Menu("💵 아이템 판매", ConsoleColor.Green, () => MenuType = ShopMenuType.SELL));
        SelectMenus.Add(new Menu("🛒 아이템 구매", ConsoleColor.Yellow, () => MenuType = ShopMenuType.BUY));
        SelectMenus.Add(new Menu("↩ 돌아가기", ConsoleColor.Cyan, () => SceneManager.ChangeScene(SceneType.START)));

        for (int i = 0; i < Shop.PageSlotCount; i++)
        {
            var shopMenu = new ShopMenu(Shop, null);
            shopMenu.MyScene = this;
            ItemMenus.Add(shopMenu);
        }
    }

    public override void Init()
    {
        UpdateItemMenu();

        switch (MenuType)
        {
            case ShopMenuType.BUY:
                Name = $"{Shop.Name} - 아이템 구매";
                Description = """
                  살 거면 사고, 말 거면 나가. 구경만 하는 손님은 질색이니까.
                """;
                break;
            case ShopMenuType.SELL:
                Name = $"{Shop.Name} - 아이템 판매";
                Description = """
                   쓰지도 않을 거면 나한테 넘겨.
                """;
                break;
        }
    }

    public override void MainDisplay()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n💰━━━━━━━━━━💰 보유 골드 💰━━━━━━━━━━💰");
        GameManager.ColorWriteLine($"{GameManager.Player.Gold} G", ConsoleColor.Yellow);
        Console.ResetColor();
        Console.WriteLine();

        ItemMenuDisplay();
    }

    public override void ItemMenuDisplay()
    {
        Console.ForegroundColor = ConsoleColor.Gray;

        if (MenuType == ShopMenuType.BUY)
        {
            Console.WriteLine("📦━━━━━━━━━━ 구매 가능한 장비 ━━━━━━━━━━📦\n");
        }
        else if (MenuType == ShopMenuType.SELL)
        {
            Console.WriteLine("📤━━━━━━━━━━ 판매 가능한 장비 ━━━━━━━━━━📤\n");
        }

        Console.ResetColor();

        ItemMenuDisplayMethod();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GameManager.ColorWriteLine($"📄 현재 페이지: {Shop.PageNumber + 1} / {Shop.PageCount}", ConsoleColor.Cyan);
    }

    void UpdateItemMenu()
    {
        int index = 0;

        foreach (var menu in ItemMenus)
        {
            if (menu is not ShopMenu shopMenu) continue;
            shopMenu.Item = null;

            if (MenuType == ShopMenuType.BUY)
            {
                Shop.ShowItemsUpdate();

                if (index < Shop.ShowItems.Count)
                {
                    shopMenu.Item = Shop.ShowItems[index];
                    shopMenu.OnSelect = () => shopMenu.Buy();
                    index++;
                }
            }
            else if (MenuType == ShopMenuType.SELL)
            {
                var inventory = GameManager.Player.Inventory;
                if (index < inventory.Count)
                {
                    shopMenu.Item = inventory[index];
                    shopMenu.OnSelect = () => shopMenu.Sell();
                    index++;
                }
            }
        }
    }
}
