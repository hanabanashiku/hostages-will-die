using Hanabanashiku.HostagesWillDie.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hanabanashiku.HostagesWillDie.UI {
    public class HudInfo : MonoBehaviour {
        public Image WeaponImage;
        public TextMeshProUGUI ShotsRemainingText;
        public TextMeshProUGUI TotalBulletsText;
        public HealthBar HealthBar;

        private Player _player;

        private void Start() {
            _player = FindObjectOfType<Player>();
            
            Debug.Assert(_player);
            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            if(_player.EquippedWeapon && _player.EquippedWeapon.Sprite) {
                WeaponImage.sprite = _player.EquippedWeapon.Sprite;
                WeaponImage.enabled = true;
            }
            else {
                WeaponImage.enabled = false;
            }

            HealthBar.Health = _player.Health;
            HealthBar.MaxHealth = _player.MaxHealth;
            ShotsRemainingText.text = _player.Ammo.ShotsRemaining.ToString();
            TotalBulletsText.text = _player.Ammo.TotalBullets.ToString();
        }
    }
}