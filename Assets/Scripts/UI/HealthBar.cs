using System;
using Hanabanashiku.GameJam.Models.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Hanabanashiku.GameJam.UI {
    public class HealthBar : MonoBehaviour {
        public float Health;
        public float MaxHealth;

        private Image _image;

        private void Awake() {
            _image = GetComponent<Image>();
            _image.type = Image.Type.Filled;
            _image.fillMethod = Image.FillMethod.Horizontal;
        }

        private void Start() {
            _image.enabled = GameManager.Instance.GameDifficulty == GameDifficulty.Easy;
        }

        private void Update() {
            var percent = Math.Max(0f, Math.Min(Health / MaxHealth, 1f));
            _image.fillAmount = percent;
        }
    }
}