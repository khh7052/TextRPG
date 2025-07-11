using TextRPG.Manager;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // UTF-8 인코딩 설정
            /*
            SaveManager saveManager = new();
            SceneManager sceneManager = new();
            ItemManager itemManager = new();
            */
            GameManager gameManager = new();
            gameManager.Play();
        }

    }
}
