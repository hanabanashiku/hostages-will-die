using System;
using Hanabanashiku.GameJam.Models.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hanabanashiku.GameJam.Entities {
    public class Player : Person {
        public int BaseSpeed = 100;
        public int SprintMultiplier = 10;

        private InputControls _input;
        private Rigidbody _rigidbody;
        private LineRenderer _lineRenderer;
        private Camera _camera;
        private bool _isSprinting;

        private int _currentSpeed {
            get {
                if(_isSprinting) {
                    return BaseSpeed * SprintMultiplier;
                }

                return BaseSpeed;
            }
        }

        protected override void Awake() {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody>();
            _lineRenderer = GetComponent<LineRenderer>();
            _camera = Camera.main;

            Debug.Assert(_rigidbody);
            Debug.Assert(_lineRenderer);
            Debug.Assert(_camera);
        }

        protected override void Die() {
            GameManager.Instance.Lose();
        }

        private void Start() {
            _input.Player.Reload.started += OnReload;
            MaxHealth = CalculateMaxHealth();
            Health = MaxHealth;
        }

        private void OnReload(InputAction.CallbackContext context) {
            StartCoroutine(Reload());
        }

        private void OnEnable() {
            _input ??= new InputControls();
            _input.Player.Enable();
        }

        private void OnDisable() {
            _input.Player.Disable();
        }

        private void Update() {
            _isSprinting = _input.Player.Sprint.IsPressed();

            // Aim Helper
            if(!IsReloading && _input.Player.Aim.IsPressed()) {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _camera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 50)));
                _lineRenderer.enabled = true;
            }
            else {
                _lineRenderer.enabled = false;
            }
        }

        private void FixedUpdate() {
            // Movement
            var movementVector = _input.Player.Movement.ReadValue<Vector2>();
            _rigidbody.velocity =
                new Vector3(movementVector.x, _rigidbody.velocity.y, movementVector.y) * _currentSpeed;

            // Fire
            if(!IsReloading && _input.Player.Fire.IsPressed()) {
                var position = transform.position;
                var mouseLocation = _camera.ScreenToWorldPoint(Input.mousePosition);
                mouseLocation.z = position.z;
                EquippedWeapon.Fire(gameObject, Ammo, Quaternion.FromToRotation(position, mouseLocation));
            }
        }

        private static float CalculateMaxHealth() {
            return GameManager.Instance.GameDifficulty switch {
                GameDifficulty.Easy => 8,
                GameDifficulty.Medium => 5,
                GameDifficulty.Hard => 3,
                GameDifficulty.ExtraHard => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(GameDifficulty))
            };
        }
    }
}