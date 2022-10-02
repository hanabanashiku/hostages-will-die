using System.Collections;
using Hanabanashiku.GameJam.Models;
using UnityEngine;

namespace Hanabanashiku.GameJam.Entities {
    public abstract class Person : MonoBehaviour {
        public Weapon EquippedWeapon;
        public Ammo Ammo;

        protected bool IsReloading;

        protected virtual void Awake() {
            Ammo = new Ammo {
                TotalBullets = 100,
                ShotsRemaining = 0
            };
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
    }
}