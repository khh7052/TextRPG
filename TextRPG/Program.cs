using TextRPG.Manager;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new();
            ItemManager itemManager = new();
            SaveManager saveManager = new();
            gameManager.Play();
        }

    }
}
