using System;
using System.Collections;
using Hanabanashiku.GameJam.Models;
using UnityEngine;

namespace Hanabanashiku.GameJam.Entities {
    public abstract class Person : MonoBehaviour {
        public Weapon EquippedWeapon;
        public Ammo Ammo;
        public float Health;
        public float MaxHealth;

        protected bool IsReloading;

        public void Damage(float damage) {
            Health = Math.Min(0, Health - damage);

            if(Health == 0) {
                Die();
            }
        }

        protected virtual void Awake() {
            Ammo = new Ammo {
                TotalBullets = 100,
                ShotsRemaining = 0
            };
        }

        protected void OnCollisionEnter(Collision collision) {
            if(collision.gameObject.CompareTag(Constants.Tags.PROJECTILE)) {
                var bullet = gameObject.GetComponent<Bullet>();
                Debug.Assert(bullet != null);
                Damage(bullet.Weapon.DamagePerShot);
            }
        }

        protected IEnumerator Reload() {
            if(IsReloading || EquippedWeapon is null || Ammo.TotalBullets == 0 ||
               Ammo.ShotsRemaining == EquippedWeapon.ShotsPerRound) {
                yield break;
            }

            IsReloading = true;
            yield return new WaitForSeconds(EquippedWeapon.ReloadTime);
            EquippedWeapon.Reload(Ammo);
            IsReloading = false;
        }

        protected abstract void Die();
    }
}