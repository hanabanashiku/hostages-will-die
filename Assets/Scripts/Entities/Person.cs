using System;
using System.Collections;
using Hanabanashiku.GameJam.Models;
using UnityEngine;

namespace Hanabanashiku.GameJam.Entities {
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Person : MonoBehaviour {
        public Weapon EquippedWeapon;
        public Ammo Ammo;
        public float Health;
        public float MaxHealth;

        protected Rigidbody Rigidbody;
        protected bool IsReloading;

        public void Damage(float damage) {
            Health = Math.Min(0, Health - damage);

            if(Health == 0) {
                Die();
            }
        }

        protected virtual void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            
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
                var bullet = collision.gameObject.GetComponent<Bullet>();
                Debug.Assert(bullet);

                if(gameObject.CompareTag(bullet.OriginTag)) {
                    return;
                }
                
                Damage(bullet.Weapon.DamagePerShot);
            }
        }

        private void Equip(Weapon weapon) {
            EquippedWeapon = weapon;
            EquippedWeapon.Shooter = gameObject;
            EquippedWeapon.AudioSource = GetComponentInChildren<AudioSource>();

            Ammo.TotalBullets += Ammo.ShotsRemaining;
            Ammo.ShotsRemaining = 0;
            EquippedWeapon.Reload(Ammo);
        }

        protected IEnumerator Reload(bool playSound = true) {
            if(IsReloading || !EquippedWeapon || Ammo.TotalBullets == 0 ||
               Ammo.ShotsRemaining == EquippedWeapon.ShotsPerRound) {
                yield break;
            }

            IsReloading = true;
            
            if(playSound) {
                StartCoroutine(EquippedWeapon.PlayReloadSound(Ammo));
            }

            yield return new WaitForSeconds(EquippedWeapon.ReloadTime);
            EquippedWeapon.Reload(Ammo);
            IsReloading = false;
        }

        protected abstract void Die();
    }
}