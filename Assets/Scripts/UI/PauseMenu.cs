using System;
using UnityEngine;

namespace Hanabanashiku.HostagesWillDie.UI {
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
            
            var rectangle = GetComponent<RectTransform>();
            rectangle.offsetMin = new Vector2(500, 100);
            rectangle.offsetMax = new Vector2(-500, -100);
        }
    }
}