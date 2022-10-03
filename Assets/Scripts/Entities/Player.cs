using System;
using Hanabanashiku.GameJam.Models.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hanabanashiku.GameJam.Entities {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(LineRenderer))]
    public class Player : Person {
        public int BaseSpeed = 100;
        public int SprintMultiplier = 10;

        private InputControls _input;
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
            _lineRenderer = GetComponent<LineRenderer>();
            _camera = Camera.main;

            Debug.Assert(Rigidbody);
            Debug.Assert(_lineRenderer);
            Debug.Assert(_camera);
        }

        protected override void Die() {
            GameManager.Instance.Lose();
        }

        private void Start() {
            _input.Player.Reload.started += OnReload;
            _input.Player.Pause.started += OnPauseGame;
            
            MaxHealth = CalculateMaxHealth();
            Health = MaxHealth;
        }

        private void OnReload(InputAction.CallbackContext context) {
            StartCoroutine(Reload());
        }

        private void OnPauseGame(InputAction.CallbackContext context) {
            GameManager.Instance.PauseGame();
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

            // Rotate character
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.up, Vector3.zero);

            if(plane.Raycast(ray, out var distance)) {
                var target = ray.GetPoint(distance);
                var direction = target - transform.position;
                var rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }
        }

        private void FixedUpdate() {
            var mouseLocation = _camera.ScreenToWorldPoint(Input.mousePosition);

            // Movement
            var movementVector = _input.Player.Movement.ReadValue<Vector2>();
            var currentTransform = transform;
            Rigidbody.velocity =
                (currentTransform.forward * movementVector.y + currentTransform.right * movementVector.x).normalized *
                _currentSpeed;

            // Fire
            if(!IsReloading && _input.Player.Fire.IsPressed()) {
                var position = transform.position;
                //var mouseLocation = _camera.ScreenToWorldPoint(Input.mousePosition);
                mouseLocation.z = position.z;
                EquippedWeapon.Fire(Ammo, Quaternion.FromToRotation(position, mouseLocation));
            }
        }

        private static float CalculateMaxHealth() {
            return GameManager.Instance.GameDifficulty switch {
                GameDifficulty.Easy => 8,
                GameDifficulty.Medium => 5,
                GameDifficulty.Hard => 3,
                GameDifficulty.Deadly => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(GameDifficulty))
            };
        }
    }
}