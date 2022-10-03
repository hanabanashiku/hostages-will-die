using System;
using System.Collections;
using Hanabanashiku.GameJam.Database;
using Hanabanashiku.GameJam.Models.Enums;
using Hanabanashiku.GameJam.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hanabanashiku.GameJam {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        
        public GameDifficulty GameDifficulty = GameDifficulty.Hard;
        public GameObject PauseMenuPrefab;
        public GameObject DialogBoxPrefab;
        public int GameTimeForRun;
        public int EnemiesKilled;

        private DialogDatabase _dialogDatabase;
        private Coroutine _gameTimer;
        
        public void Awake() {
            Instance = this;
            _dialogDatabase = new DialogDatabase();

            DontDestroyOnLoad(gameObject);
        }

        public void StarTimer() {
            StartCoroutine(StartBodyTimer());
        }

        public void Lose() {
            SceneManager.LoadScene(Constants.Scenes.LOSE);
        }

        public void PauseGame() {
            var canvas = GetOrCreateCanvas();
             Instantiate(PauseMenuPrefab, canvas.transform, true);
        }

        public void ShowDialog(int conversationId, OnDialogFinish onFinish = null) {
            var dialog = _dialogDatabase.GetConversation(conversationId);
            var canvas = GetOrCreateCanvas();
            var dialogBox = Instantiate(DialogBoxPrefab, canvas.transform, true);
            var dialogData = dialogBox.GetComponent<DialogBox>();
            if(onFinish is not null) {
                dialogData.OnDialogFinish += onFinish;
            }
            dialogData.VoiceLines = dialog;
        }

        private IEnumerator StartBodyTimer() {
            while(true) {
                yield return new WaitForSeconds(1f);
                GameTimeForRun += 1;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private static Canvas GetOrCreateCanvas() {
            var canvas = FindObjectOfType<Canvas>();

            if(!canvas) {
                var obj = new GameObject();
                canvas = obj.AddComponent<Canvas>();
            }

            return canvas;
        }
    }
}