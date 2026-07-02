using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1shipBattle.classes.Equipment;
using static Task1shipBattle.Armor;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{

    public class Ammunition : Equipment
    {
        public AmmoType Type { get; }
        public Single BaseDamage { get; }
        public Single PenetrationMultiplier { get; } 
        public Int32 FlightTurns { get; }

        private Ammunition(AmmoType type, Single baseDamage, Single multiplier, Int32 weight, Int32 flightTurns = 0)
        : base(weight)
        {
            Type = type;
            BaseDamage = baseDamage;
            PenetrationMultiplier = multiplier;
            FlightTurns = flightTurns;
        }

        public static Ammunition CreateArmorPiercing()
        {
            return new Ammunition(AmmoType.AP, 25f, 1.0f, weight: 10);
        }

        public static Ammunition CreateHighExplosive()
        {
            return new Ammunition(AmmoType.HE, 15f, 1.5f, weight: 8); 
        }

        public static Ammunition CreateTorpedo()
        {
            return new Ammunition(AmmoType.Torped, 20f, 2.0f, flightTurns: 1, weight: 25);
        }

        public String GetName()
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
