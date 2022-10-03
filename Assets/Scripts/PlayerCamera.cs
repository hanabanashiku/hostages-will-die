using Hanabanashiku.HostagesWillDie.Entities;
using UnityEngine;

namespace Hanabanashiku.HostagesWillDie {
    public class PlayerCamera : MonoBehaviour {
        public float FollowDistance = 10;

        private Player _player;

        private void Start() {
            _player = FindObjectOfType<Player>();
            transform.rotation = Quaternion.Euler(45, 0, 0);
        }

        private void Update() {
            var currentTransform = transform;
            var playerPosition = _player.transform.position;
            currentTransform.position =
                new Vector3(playerPosition.x, playerPosition.y + FollowDistance, playerPosition.z - FollowDistance);
        }
    }
}