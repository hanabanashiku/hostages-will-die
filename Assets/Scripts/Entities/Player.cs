using UnityEngine;
using UnityEngine.InputSystem;

namespace Hanabanashiku.GameJam.Entities {
    public class Player : MonoBehaviour, InputControls.IPlayerActions {
        public int BaseSpeed = 100;
        public int SprintMultiplier = 10;
        
        private InputControls _input;
        private Rigidbody _rigidbody;
        private bool _isSprinting;

        private int _currentSpeed {
            get {
                if(_isSprinting) {
                    return BaseSpeed * SprintMultiplier;
                }

                return BaseSpeed;
            }
        }

        public void OnMovement(InputAction.CallbackContext context) {}

        public void OnSprint(InputAction.CallbackContext context) {
            _isSprinting = context.ReadValueAsButton();
        }

        public void OnAim(InputAction.CallbackContext context) {
            throw new System.NotImplementedException();
        }

        public void OnFire(InputAction.CallbackContext context) {
            throw new System.NotImplementedException();
        }

        public void OnAssassinate(InputAction.CallbackContext context) {
            throw new System.NotImplementedException();
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void OnEnable() {
            _input ??= new InputControls();
            _input.Player.SetCallbacks(this);
            _input.Player.Enable();
        }

        private void OnDisable() {
            _input.Player.Disable();
        }

        private void FixedUpdate() {
            var movementVector = _input.Player.Movement.ReadValue<Vector2>();
            _rigidbody.velocity =
                new Vector3(movementVector.x, _rigidbody.velocity.y, movementVector.y) * _currentSpeed;
        }
    }
}
