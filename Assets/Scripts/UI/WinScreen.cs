using TMPro;
using UnityEngine;

namespace Hanabanashiku.HostagesWillDie.UI {
    public class WinScreen : EndingScreen {
        public TextMeshProUGUI HostagesLeftUI;
        public TextMeshProUGUI TimeElapsedUI;
        public TextMeshProUGUI EnemiesKilled;

        protected override void Start() {
            base.Start();

            HostagesLeftUI.text = HostagesLeft.ToString();
            EnemiesKilled.text = TotalEnemiesKilled.ToString();
        }
    }
}