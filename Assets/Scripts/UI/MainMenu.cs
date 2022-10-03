using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hanabanashiku.HostagesWillDie.UI {
    public class MainMenu : MonoBehaviour {

        public void StartGame() {
            SceneManager.LoadScene(Constants.Scenes.OPEN_DIALOG);
        }
        
        public void ExitGame() {
            Application.Quit();
        }
    }
}

