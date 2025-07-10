using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TextRPG.Manager
{
    public class SaveFile
    {
        public string FilePath { get; set; } // 저장 파일 경로
    }

    internal class SaveManager
    {

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

        public SaveFile[] SaveFiles
        {
            get
            {
                return _saveFiles;
            }
        }
 
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


        public void Save(SaveFile saveFile)
        {
            string json = JsonSerializer.Serialize(GameManager.Player);
            File.WriteAllText(saveFile.FilePath, json);
            GameManager.DisplayMessage("저장 파일이 성공적으로 저장되었습니다.");
        }

        public void Load(SaveFile saveFile)
        {
            if (!File.Exists(saveFile.FilePath))
            {
                GameManager.DisplayWarning("저장 파일이 존재하지 않습니다.");
                return;
            }

            string json = File.ReadAllText(saveFile.FilePath);
            GameManager.Player = JsonSerializer.Deserialize<Character>(json);
            GameManager.DisplayMessage("저장 파일이 성공적으로 불러와졌습니다.");
        }

        public void Delete(SaveFile saveFile)
        {
            if (File.Exists(saveFile.FilePath))
            {
                File.Delete(saveFile.FilePath);
                GameManager.DisplayMessage("저장 파일이 삭제되었습니다.");
            }
            else
            {
                GameManager.DisplayWarning("저장 파일이 존재하지 않습니다.");
            }
        }

        public void DisplaySaveFiles()
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                string info = GetSaveFileInfo(_saveFiles[i].FilePath);
                Console.WriteLine(info);
            }
        }

        public static string GetSaveFileInfo(string filePath)
        {
            string info;

            if (File.Exists(filePath))
            {
                info = $"{filePath} (저장됨)";
            }
            else
            {
                info = $"{filePath} (비어있음)";
            }

            return info;
        }
    }
}
