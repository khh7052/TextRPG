using TextRPG.Manager;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // UTF-8 인코딩 설정

            GameManager gameManager = new();
            ItemManager itemManager = new();
            SaveManager saveManager = new();
            gameManager.Play();
        }

    }
}
