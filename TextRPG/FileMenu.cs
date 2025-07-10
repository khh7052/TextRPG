using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Manager;

namespace TextRPG
{
    internal class FileMenu : Menu
    {
        private SaveFile _saveFile;

        public FileMenu(SaveFile saveFile, string content = "", ConsoleColor color = ConsoleColor.White, Action onSelect = null) : base(content, color, onSelect)
        {
            _saveFile = saveFile;
            isSelected = false;
        }

        public override void Display()
        {
            if (!Enable) return;

            if (isSelected)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black; // 선택되지 않은 메뉴는 검은색 배경으로 표시
                Console.ForegroundColor = Color;
            }

            // 메뉴 내용 출력
            Content = $"📂 {SaveManager.GetSaveFileInfo(_saveFile.FilePath)}";
            string content = isSelected ? $"▶   {Content}" : Content;
            Console.WriteLine(content);
            Console.ResetColor(); // 색상 초기화
        }

        public override void Select()
        {
            OnSelect?.Invoke(); // 파일 메뉴 선택 시 등록된 액션 호출
        }

        public void Save()
        {

           if (_saveFile == null) return;

            // 저장 로직 구현
            SaveManager.Instance.Save(_saveFile);
        }
        public void Load()
        {
            if (_saveFile == null) return;

            // 불러오기 로직 구현
            SaveManager.Instance.Load(_saveFile);
        }

        public void Delete()
        {
            if (_saveFile == null) return;

            // 삭제 로직 구현
            SaveManager.Instance.Delete(_saveFile);
        }
    }
}
