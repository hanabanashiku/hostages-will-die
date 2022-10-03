using UnityEngine;

namespace Hanabanashiku.GameJam.Entities {
    public class Enemy : Person {
        private Collider _target;
        private Camera _camera;

        private bool _playerIsInRange {
            get {
                var planes = GeometryUtility.CalculateFrustumPlanes(_camera);

                return GeometryUtility.TestPlanesAABB(planes, _target.bounds);
            }
        }

        protected override void Die() {
            GameManager.Instance.EnemiesKilled++;
            Destroy(gameObject);
        }

        private void Start() {
            _target = FindObjectOfType<Player>().gameObject.GetComponent<Collider>();
            _camera = GetComponentInChildren<Camera>();
        }

        private void Update() {
            if(Ammo.ShotsRemaining == 0) {
                StartCoroutine(Reload(false));
            }

            if(!IsReloading && _playerIsInRange) {
                var player = _target.transform.position;

                // todo debug
                transform.LookAt(player);

                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit,
                       Mathf.Infinity, 1 << 8)) {
                    if(hit.collider.gameObject.CompareTag(Constants.Tags.PLAYER)) {
                        EquippedWeapon.Fire(Ammo);
                    }
                }
            }
        }
    }
}