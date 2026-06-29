using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;
using static Task1shipBattle.Armor;

namespace Task1shipBattle
{

    public class Ammunition
    {
        public AmmoType Type { get; }
        public float BaseDamage { get; }
        public float PenetrationMultiplier { get; } 
        public int FlightTurns { get; } 

        private Ammunition(AmmoType type, float baseDamage, float multiplier, int flightTurns = 0)
        {
            Type = type;
            BaseDamage = baseDamage;
            PenetrationMultiplier = multiplier;
            FlightTurns = flightTurns;
        }

        public static Ammunition CreateArmorPiercing()
        {
            return new Ammunition(AmmoType.AP, 25f, 1.0f);
        }

        public static Ammunition CreateHighExplosive()
        {
            return new Ammunition(AmmoType.HE, 15f, 1.5f); 
        }

        public static Ammunition CreateTorpedo()
        {
            return new Ammunition(AmmoType.Torped, 20f, 2.0f, flightTurns: 1);
        }

        public string GetName()
        {
            switch (Type)
            {
                case AmmoType.AP: return "Бронебойный снаряд";
                case AmmoType.HE: return "Фугасный снаряд";
                case AmmoType.Torped: return "Торпеда";
                default: return "Неизвестный снаряд";
            }
        }
    }


}
