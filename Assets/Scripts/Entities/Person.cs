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
            if(EquippedWeapon) {
                Equip(EquippedWeapon);
                Ammo = new Ammo {
                    ShotsRemaining = EquippedWeapon.ShotsPerRound,
                    TotalBullets = EquippedWeapon.TotalShots - EquippedWeapon.ShotsPerRound
                };
            }
        }

        protected void OnCollisionEnter(Collision collision) {
            if(collision.gameObject.CompareTag(Constants.Tags.PROJECTILE)) {
                var bullet = gameObject.GetComponent<Bullet>();
                Debug.Assert(bullet);
                Damage(bullet.Weapon.DamagePerShot);
            }
        }

        private void Equip(Weapon weapon) {
            EquippedWeapon = weapon;
            EquippedWeapon.Shooter = gameObject;
            EquippedWeapon.AudioSource = GetComponentInChildren<AudioSource>();

            Ammo.TotalBullets += Ammo.ShotsRemaining;
            Ammo.ShotsRemaining = 0;
            EquippedWeapon.Reload(Ammo, PlaySound: false);
        }

        protected IEnumerator Reload() {
            if(IsReloading || !EquippedWeapon || Ammo.TotalBullets == 0 ||
               Ammo.ShotsRemaining == EquippedWeapon.ShotsPerRound) {
                yield break;
            }

            IsReloading = true;
            StartCoroutine(EquippedWeapon.PlayReloadSound(Ammo));
            yield return new WaitForSeconds(EquippedWeapon.ReloadTime);
            EquippedWeapon.Reload(Ammo);
            IsReloading = false;
        }

        protected abstract void Die();
    }
}