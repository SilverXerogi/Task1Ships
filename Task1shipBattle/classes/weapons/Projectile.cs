using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class Projectile
    {
        public Ammunition Ammo { get; }
        public Armor TargetArmor { get; }
        public Int32 TurnsRemaining { get; private set; }

        public Projectile(Ammunition ammo, Armor targetArmor, Int32 flightTurns)
        {
            Ammo = ammo;
            TargetArmor = targetArmor;
            TurnsRemaining = flightTurns;
        }

        public void UpdateFlight()
        {
            if (TurnsRemaining > 0)
                TurnsRemaining--;
        }

        public Boolean HasArrived => TurnsRemaining <= 0;
    }
}
