using System;
using Hanabanashiku.GameJam.Database;
using Hanabanashiku.GameJam.Models.Enums;
using Hanabanashiku.GameJam.UI;
using UnityEngine;

namespace Hanabanashiku.GameJam {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        
        public GameDifficulty GameDifficulty = GameDifficulty.Hard;
        public GameObject PauseMenuPrefab;
        public GameObject DialogBoxPrefab;

        private DialogDatabase _dialogDatabase;
        
        public void Awake() {
            Instance = this;
            _dialogDatabase = new DialogDatabase();

            DontDestroyOnLoad(gameObject);
        }

        public void Lose() {
            // TODO
            // Record loss
            // Show loss modal
            // Reload scene
            // Move to last checkpoint
            throw new NotImplementedException();
        }

        public void PauseGame() {
            var canvas = GetOrCreateCanvas();
             Instantiate(PauseMenuPrefab, canvas.transform, true);
        }

        public void ShowDialog(int conversationId) {
            var dialog = _dialogDatabase.GetConversation(conversationId);
            var canvas = GetOrCreateCanvas();
            var dialogBox = Instantiate(DialogBoxPrefab, canvas.transform, true);
            var dialogData = dialogBox.GetComponent<DialogBox>();
            dialogData.VoiceLines = dialog;
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