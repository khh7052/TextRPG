﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Scene; // Scene 네임스페이스 추가

namespace TextRPG.Manager
{
    enum SceneType
    {
        START,// 시작 화면
        STATUS, // 상태 화면
        INVENTORY, // 인벤토리 화면
        SHOP, // 상점 화면
        DUNGEON, // 던전 화면
        REST, // 휴식 화면
        SAVE, // 저장 화면
        GAMEOVER // 게임 오버 화면
    }

    internal class SceneManager
    {

        private static SceneManager _instance;
        public static SceneManager Instance
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

        private static Dictionary<SceneType, SceneBase> _scenes = new(); // 씬 목록

        public static SceneBase CurrentScene { get; private set; } // 현재 씬

        public static Action<SceneBase> OnChangeScene { get; set; } // 씬 변경 이벤트


        public SceneManager()
        {
            _instance = this;
            // 씬 초기화
            AddScene(SceneType.START, new StartScene());
            AddScene(SceneType.STATUS, new StatusScene());
            AddScene(SceneType.INVENTORY, new InventoryScene());
            AddScene(SceneType.SHOP, new ShopScene());
            AddScene(SceneType.DUNGEON, new DungeonScene());
            AddScene(SceneType.REST, new RestScene());
            AddScene(SceneType.SAVE, new SaveScene());
            AddScene(SceneType.GAMEOVER, new GameOverScene());

            CurrentScene = _scenes[SceneType.START]; // 기본 씬 설정
        }

        public void StartScene()
        {
            CurrentScene.Start();
        }

        // 현재 씬 실행
        public void PlayScene()
        {
            CurrentScene.Update();
        }

        // 씬 추가
        public void AddScene(SceneType sceneType, SceneBase scene)
        {
            if (!_scenes.ContainsKey(sceneType))
            {
                _scenes.Add(sceneType, scene);
            }
        }

        public static void ChangeScene(SceneType sceneType)
        {
            if (_scenes.ContainsKey(sceneType))
            {
                CurrentScene = _scenes[sceneType];
                CurrentScene.Start();
                OnChangeScene?.Invoke(CurrentScene); // 씬 변경 이벤트 호출
            }
            else
            {
                Console.WriteLine("존재하지 않는 씬입니다.");
            }
        }
    }
}
