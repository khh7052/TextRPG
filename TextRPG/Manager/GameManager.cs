using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Manager
{
    internal class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }
        
        public static Character Player { get; set; }

        public GameManager()
        {
            _instance = this;
            Player = new();
        }

        public void Play()
        {
            int selection = 0;
            while (true)
            {
                do
                {
                    SceneManager.Instance.PlayScene();
                } while (!ReadInt(out selection));
                SceneManager.CurrentScene.SelectMenu(selection);
            }
        }

        public void Rest()
        {
            if (Player.Gold < 500)
            {
                DisplayWarning($"골드가 부족합니다. {500 - Player.Gold} 가 더 필요합니다.");
                return;
            }

            Player.Gold -= 500; // 골드 차감
            Player.HP = Player.MaxHP; // 체력 회복

            DisplayMessage("휴식을 취하여 체력이 회복되었습니다.");
        }

        public bool ReadInt(out int result)
        {
            bool isValid = true;

            if (!int.TryParse(Console.ReadLine(), out result))
            {
                isValid = false;
            }

            if (!isValid)
            {
                DisplayWarning("잘못된 입력입니다. 숫자를 입력해주세요.");
            }

            return isValid;
        }

        public static void ColorWriteLine(string content, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(content);
            Console.ResetColor();
        }

        // 메시지 출력
        public static void DisplayMessage(string content, int delay = 1000)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(content);
            Console.ResetColor();
            Thread.Sleep(delay);
        }

        // 경고 메시지 출력
        public static void DisplayWarning(string content, int delay = 1200)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(content);
            Console.ResetColor();
            Thread.Sleep(delay);
        }

    }
}
