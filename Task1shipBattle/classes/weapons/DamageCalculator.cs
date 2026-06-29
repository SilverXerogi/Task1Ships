using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle.classes.weapons
{
    public static class DamageCalculator
    {
        public static float CalculateHit(Gun gun, Ammunition ammo, Armor targetArmor)
        {
            float baseDamage = ammo.BaseDamage;

            if (ammo.Type == AmmoType.AP && targetArmor is antiTorped)
            {
                return 0f; 
            }

            if (gun.IgnoresArmor)
            {
                if (targetArmor is antiTorped)
                {
                    return baseDamage * ammo.PenetrationMultiplier;
                }
                return baseDamage;
            }

            bool isPenetration = gun.PenetratesAllArmor ||
                                 gun.PenetratesArmor == GetArmorType(targetArmor);

            if (isPenetration)
            {
                return baseDamage * ammo.PenetrationMultiplier;
            }
            else
            {
                float protection = targetArmor.GetDef(ammo.Type);
                return baseDamage * (1 - protection);
            }
        }

        private static ArmorType GetArmorType(Armor armor)
        {
            if (armor is ArmoredBelt) return ArmorType.ArmoredBelt;
            if (armor is Kazemat) return ArmorType.Kazemat;
            if (armor is antiTorped) return ArmorType.antiTorped;
            throw new ArgumentException("Неизвестный тип брони");
        }
    }
}
