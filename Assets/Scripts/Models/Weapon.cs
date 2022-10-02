using System;
using System.Collections;
using Hanabanashiku.GameJam.Entities;
using Hanabanashiku.GameJam.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Hanabanashiku.GameJam.Models {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/New Weapon", order = 1)]
    public class Weapon : ScriptableObject {
        public string Name;
        public Sprite Sprite;
        public float FireRate; // shots/sec
        public float DamagePerShot;
        public bool IsSilenced;
        public int ShotsPerRound;
        public int TotalShots;
        public float MaxShotDistance;
        public float ReloadTime;
        public AudioClip FireSound;
        public AudioClip ReloadSound;

        [NonSerialized]
        public GameObject Shooter;
        [NonSerialized]
        public AudioSource AudioSource;
        
        private float _nextTimeToFire;

        public void OnEnable() {
            _nextTimeToFire = Time.time + 1.0f;
        }

        public void Fire(Ammo ammo, Quaternion direction) {
            if(ammo.ShotsRemaining < 1 || Time.time < _nextTimeToFire) {
                return;
            }

            AudioSource.PlayOneShot(FireSound, Constants.SHOT_VOLUME);
            Bullet.InstantiateBullet(Shooter, this, direction);
            ammo.ShotsRemaining--;

            _nextTimeToFire = Time.time + 1 / FireRate;
        }

        public void Reload(Ammo ammo, bool PlaySound = true) {
            var originalChamber = ammo.ShotsRemaining;
            ammo.ShotsRemaining = Math.Min(ShotsPerRound, originalChamber + Math.Max(0, ammo.TotalBullets - ammo.ShotsRemaining));
            ammo.TotalBullets -= ammo.ShotsRemaining - originalChamber;
        }

        public IEnumerator PlayReloadSound() {
            yield return AudioSource.LoopClip(ReloadSound, ReloadSound.length < 0.5f ? ShotsPerRound : 1);
        }
    }
}