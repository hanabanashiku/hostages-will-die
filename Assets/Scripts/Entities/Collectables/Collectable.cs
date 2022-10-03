using UnityEngine;

namespace Hanabanashiku.GameJam.Entities.Collectables {
    public abstract class Collectable : MonoBehaviour {
        protected abstract bool Apply(Player player);
        
        private void OnTriggerEnter(Collider other) {
            if(!other.CompareTag(Constants.Tags.PLAYER)) {
                return;
            }

            var player = other.gameObject.GetComponent<Player>();
            if(Apply(player)) {
                Destroy(gameObject);
            }
        }
    }
}