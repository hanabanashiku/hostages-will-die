using System;
using Hanabanashiku.HostagesWillDie.Models.Enums;

namespace Hanabanashiku.HostagesWillDie.Entities.Collectables {
    public class HealthBox : Collectable {
        public int HealthToRestore = -1;
        
        protected override bool Apply(Player player) {
            if(player.Health >= player.MaxHealth) {
                return false;
            }

            player.Health = Math.Min(player.MaxHealth, player.Health + HealthToRestore);
            
            // Dispose of collectible
            return true;
        }
        
        private void Awake() {
            if(HealthToRestore < 1) {
                HealthToRestore = GetDefaultHealth();
            }
        }

        private static int GetDefaultHealth() {
            return GameManager.Instance.GameDifficulty switch {
                GameDifficulty.Easy => 10,
                GameDifficulty.Medium => 3,
                GameDifficulty.Hard => 2,
                GameDifficulty.Deadly => 1,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}