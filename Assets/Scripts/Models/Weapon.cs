using System;
using Hanabanashiku.GameJam.Entities;
using UnityEngine;

namespace Hanabanashiku.GameJam.Models {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/New Weapon", order = 1)]
    public class Weapon : ScriptableObject {
        public string Name;
        public float FireRate; // shots/sec
        public float DamagePerShot;
        public bool IsSilenced;
        public int ShotsPerRound;
        public int TotalShots;
        public float MaxShotDistance;
        public float ReloadTime;

        private float _nextTimeToFire;

        public void OnEnable() {
            _nextTimeToFire = Time.time + 1.0f;
        }

        public void Fire(GameObject shooter, Ammo ammo, Quaternion direction) {
            if(ammo.ShotsRemaining < 1 || Time.time < _nextTimeToFire) {
                return;
            }

            Bullet.InstantiateBullet(shooter, this, direction);
            ammo.ShotsRemaining--;

            _nextTimeToFire = Time.time + 1 / FireRate;
        }

        public void Reload(Ammo ammo) {
            var originalChamber = ammo.ShotsRemaining;
            ammo.ShotsRemaining = Math.Min(ShotsPerRound, originalChamber + Math.Max(0, ammo.TotalBullets - ammo.ShotsRemaining));
            ammo.TotalBullets -= ammo.ShotsRemaining - originalChamber;
        }
    }
}