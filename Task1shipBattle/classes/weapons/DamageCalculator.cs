using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;

namespace Task1shipBattle.classes.weapons
{
    public static class DamageCalculator
    {
        public static Single CalculateHit(Gun gun, Ammunition ammo, Armor targetArmor)
        {
            Single gunDamage = gun.GetRandomDamage();
            Single ammoDamage = ammo.BaseDamage;
            Single totalBaseDamage = gunDamage + ammoDamage;

            if (ammo.Type == AmmoType.AP && targetArmor is AntiTorped)
            {
                return 0f; 
            }

            if (gun.IgnoresArmor)
            {
                if (targetArmor is AntiTorped)
                {
                    return totalBaseDamage * ammo.PenetrationMultiplier;
                }
                return totalBaseDamage;
            }

            Boolean isPenetration = gun.PenetratesAllArmor ||
                                 gun.PenetratesArmor == GetArmorType(targetArmor);

            if (isPenetration)
            {
                return totalBaseDamage * ammo.PenetrationMultiplier;
            }
            else
            {
                Single protection = targetArmor.GetDef(ammo.Type);
                return totalBaseDamage * (1 - protection);
            }
        }

        private static ArmorType GetArmorType(Armor armor)
        {
            if (armor is ArmoredBelt) return ArmorType.ArmoredBelt;
            if (armor is Kazemat) return ArmorType.Kazemat;
            if (armor is AntiTorped) return ArmorType.antiTorped;
            throw new ArgumentException("Неизвестный тип брони");
        }
    }
}
