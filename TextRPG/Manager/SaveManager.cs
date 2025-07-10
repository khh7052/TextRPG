using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TextRPG.Manager
{
    internal class SaveManager
    {
        public class SaveFile
        {
            public string FilePath { get; set; } // 저장 파일 경로
        }

        public SaveManager()
        {
            Initialize();
        }

        private static SaveManager _instance;
        public static SaveManager Instance
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
        
        private SaveFile[] _saveFiles = new SaveFile[4]; // 저장 파일 목록
        private string _saveDirectory = "Saves"; // 저장 파일 디렉토리

        public int SaveFileLength => _saveFiles.Length; // 저장 파일 개수

        public void Initialize()
        {
            _instance = this;
            if (!Directory.Exists(_saveDirectory))
            {
                Directory.CreateDirectory(_saveDirectory);
            }

            // 저장 파일 초기화
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                _saveFiles[i] = new SaveFile
                {
                    FilePath = Path.Combine(_saveDirectory, $"{i + 1}.json"),
                };
            }
        }


        public string GetFilePath(int idx)
        {
            if (idx < 0 || idx >= _saveFiles.Length)
                throw new ArgumentOutOfRangeException(nameof(idx), "유효하지 않은 저장 파일 인덱스입니다.");

            return _saveFiles[idx].FilePath;
        }


        public void Save(int idx)
        {
            if (idx < 0 || idx >= _saveFiles.Length)
                throw new ArgumentOutOfRangeException(nameof(idx), "유효하지 않은 저장 파일 인덱스입니다.");


            string json = JsonSerializer.Serialize(GameManager.Player);
            File.WriteAllText(GetFilePath(idx), json);
            GameManager.DisplayMessage("저장 파일이 성공적으로 저장되었습니다.");
        }

        public void Load(int idx)
        {
            if (idx < 0 || idx >= _saveFiles.Length)
                throw new ArgumentOutOfRangeException(nameof(idx), "유효하지 않은 저장 파일 인덱스입니다.");

            string filePath = GetFilePath(idx);

            if (!File.Exists(filePath))
            {
                GameManager.DisplayWarning("저장 파일이 존재하지 않습니다.");
                return;
            }

            string json = File.ReadAllText(filePath);
            GameManager.Player = JsonSerializer.Deserialize<Character>(json);
            GameManager.DisplayMessage("저장 파일이 성공적으로 불러와졌습니다.");
        }

        public void Delete(int idx)
        {
            if (idx < 0 || idx >= _saveFiles.Length)
                throw new ArgumentOutOfRangeException(nameof(idx), "유효하지 않은 저장 파일 인덱스입니다.");

            string filePath = GetFilePath(idx);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                GameManager.DisplayMessage("저장 파일이 삭제되었습니다.");
            }
            else
            {
                throw new FileNotFoundException("저장 파일이 존재하지 않습니다.", filePath);
            }
        }

        public void DisplaySaveFiles()
        {
            Console.WriteLine("[저장 파일 목록]");
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                string filePath = _saveFiles[i].FilePath;
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"{i + 1}. {filePath} (저장됨)");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {filePath} (비어있음)");
                }
            }
        }
    }
}
