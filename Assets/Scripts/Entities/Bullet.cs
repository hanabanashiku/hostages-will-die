using System;
using Hanabanashiku.HostagesWillDie.Models;
using UnityEngine;

namespace Hanabanashiku.HostagesWillDie.Entities {
    [RequireComponent(typeof(BoxCollider))]
    public class Bullet : MonoBehaviour {
        public Weapon Weapon { get; set; }
        public string OriginTag;

        [SerializeField] private int _speed = 1000;
        private Vector3 _origin;
        private static GameObject prefab;

        private void Awake() {
            _origin = transform.position;
        }

        private void Update() {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
            DestroyIfTooFar();
        }

        private void OnCollisionEnter(Collision _) {
            Destroy(gameObject);
        }

        private void DestroyIfTooFar() {
            if(Vector3.Distance(_origin, transform.position) < Weapon.MaxShotDistance) {
                return;
            }

            Destroy(gameObject);
        }

        public static void InstantiateBullet(GameObject origin, Weapon weapon) {
            prefab ??= (GameObject)Resources.Load("Bullet");
            var obj = Instantiate(prefab, origin.transform.position + (Vector3.up * 4) + (Vector3.forward), origin.transform.rotation);
            var bullet = obj.GetComponent<Bullet>();
            bullet.Weapon = weapon;
            bullet.OriginTag = origin.tag;
        }
    }
}
