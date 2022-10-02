using System;
using Hanabanashiku.GameJam.Models.Enums;
using UnityEngine;

namespace Hanabanashiku.GameJam {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        
        public GameDifficulty GameDifficulty = GameDifficulty.Hard;
        public GameObject PauseMenuPrefab;
        
        public void Awake() {
            Instance = this;
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
            var canvas = FindObjectOfType<Canvas>();

            if(!canvas) {
                var obj = new GameObject();
                canvas = obj.AddComponent<Canvas>();
            }

            var pauseMenu = Instantiate(PauseMenuPrefab, canvas.transform, true);
            var rectangle = pauseMenu.GetComponent<RectTransform>();
            rectangle.offsetMin = new Vector2(500, 100);
            rectangle.offsetMax = new Vector2(-500, -100);
        }
    }
}