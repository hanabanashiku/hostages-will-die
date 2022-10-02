using System;
using UnityEngine;

namespace Hanabanashiku.GameJam.UI {
    public class PauseMenu : MonoBehaviour {
        public void OnContinue() {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
        
        public void OnQuit() {
            Application.Quit();
        }

        private void Start() {
            Time.timeScale = 0f;
        }
    }
}