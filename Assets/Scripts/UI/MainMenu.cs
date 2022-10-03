using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hanabanashiku.GameJam.UI {
    public class MainMenu : MonoBehaviour {

        public void StartGame() {
            SceneManager.LoadScene(Constants.Scenes.OPEN_DIALOG);
        }
        
        public void ExitGame() {
            Application.Quit();
        }
    }
}

