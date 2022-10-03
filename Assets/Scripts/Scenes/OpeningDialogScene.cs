using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hanabanashiku.HostagesWillDie.Scenes {
    public class OpeningDialogScene : MonoBehaviour {
        private void Start() {
            GameManager.Instance.ShowDialog(Constants.Dialogues.OPEN_DIALOG, OnFinish);
        }

        private static void OnFinish() {
            SceneManager.LoadScene(Constants.Scenes.SCENE_1);
        }
    }
}