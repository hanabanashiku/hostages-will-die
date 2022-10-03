using UnityEngine;

namespace Hanabanashiku.GameJam.Entities {
    public class Enemy : Person {
        private Transform _target;
        private bool _playerIsInRange;
        
        protected override void Die() {
            GameManager.Instance.EnemiesKilled++;
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag(Constants.Tags.PLAYER)) {
                _playerIsInRange = true;
                _target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag(Constants.Tags.PLAYER)) {
                _playerIsInRange = false;
                _target = null;
            }
        }

        private void Update() {
            if(Ammo.ShotsRemaining == 0) {
                StartCoroutine(Reload(false));
            }
            
            if (!IsReloading && _playerIsInRange) {
                transform.rotation =  Quaternion.LookRotation(_target.position - transform.position, transform.up);
                EquippedWeapon.Fire(Ammo, Quaternion.identity);
            }
        }
    }
}