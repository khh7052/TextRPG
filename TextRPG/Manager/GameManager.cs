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

        private MenuController _menuController = new(); // 메뉴 컨트롤러 인스턴스

        public GameManager()
        {
            _instance = this;
            Player = new();
        }

        public void Play()
        {
            SceneManager.Instance.StartScene();

            while (true)
            {
                SceneManager.Instance.PlayScene();
                _menuController.Input();
            }
        }

        public static void GameOver()
        {
            DisplayWarning("게임 오버! 다시 시작합니다.");
            Player = new Character(); // 플레이어 초기화
            SceneManager.ChangeScene(SceneType.START); // 시작 씬으로 변경
        }

        public void Rest()
        {
            if (Player.Gold < 500)
            {
                DisplayWarning($"골드가 부족합니다. {500 - Player.Gold} 가 더 필요합니다.");
                return;
            }

            if(Player.HP >= Player.MaxHP)
            {
                DisplayWarning("이미 체력이 가득 찼습니다.");
                return;
            }

            Player.Gold -= 500; // 골드 차감
            Player.HP += 100; // 체력 회복

            DisplayMessage("휴식을 취하여 체력이 회복되었습니다.");
        }

        public static void ExitGame()
        {
            Environment.Exit(0); // 프로그램 종료
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

        public static void DisplayLine(ConsoleColor color = ConsoleColor.White)
        {
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }

        public static void DisplayEnter(int count)
        {
            for (int i = 0; i < count; i++)
                Console.WriteLine();
        }

    }
}
