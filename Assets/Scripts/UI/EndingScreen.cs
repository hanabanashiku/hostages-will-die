using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hanabanashiku.HostagesWillDie.UI {
    public class EndingScreen : MonoBehaviour {
        public int TotalEnemiesKilled;
        public int GameTime;
        public int HostagesLeft;
        
        public void OnClickMenuButton() {
            SceneManager.LoadScene(Constants.Scenes.MAIN_MENU);
        }

        protected virtual void Start() {
            var hostageCounter = FindObjectOfType<HostageCounter>();
            var gm = GameManager.Instance;
            TotalEnemiesKilled = gm.EnemiesKilled;
            GameTime = gm.GameTimeForRun;
            HostagesLeft = hostageCounter.HostagesRemaining;
            
            Destroy(GameManager.Instance.gameObject);
            Destroy(FindObjectOfType<HudInfo>().gameObject);
        }
    }
}