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
        public Ship Target { get; set; }
        public Ship Attacker { get; }
        public Gun Gun { get; }
        public int TurnsRemaining { get; private set; }

        public Projectile(Ammunition ammo, Ship attacker, Ship target, Gun gun, int flightTurns)
        {
            Ammo = ammo;
            Attacker = attacker;
            Target = target;
            Gun = gun;
            TurnsRemaining = flightTurns;
        }

        public void UpdateFlight()
        {
            if (TurnsRemaining > 0)
                TurnsRemaining--;
        }

        public bool HasArrived => TurnsRemaining <= 0;
    }
}