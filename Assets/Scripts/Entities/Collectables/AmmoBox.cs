using System;

namespace Hanabanashiku.HostagesWillDie.Entities.Collectables {
    public sealed class AmmoBox : Collectable {
        public int AmmoAmount = 200;
        
        protected override bool Apply(Player player) {
            var weapon = player.EquippedWeapon;
            var ammo = player.Ammo;

            // shots in clips + in the chamber
            var totalBullets = ammo.ShotsRemaining + ammo.TotalBullets;

            if(totalBullets >= weapon.TotalShots) {
                // Don't waste the collectable, nothing to gain here
                return false;
            }

            var bulletsToGive = Math.Min(weapon.TotalShots - totalBullets, AmmoAmount);
            ammo.TotalBullets += bulletsToGive;
            
            // Dispose of collectable
            return true;
        }
    }
}