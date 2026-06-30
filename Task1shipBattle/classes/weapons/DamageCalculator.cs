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
        public static float CalculateHit(Gun gun, Ammunition ammo, Armor targetArmor)
        {
            float gunDamage = gun.GetRandomDamage();
            float ammoDamage = ammo.BaseDamage;
            float totalBaseDamage = gunDamage + ammoDamage;

            if (ammo.Type == AmmoType.AP && targetArmor is AntiTorped)
            {
                return 0f;
            }

            if (ammo.Type == AmmoType.Torped)
            {
                if (targetArmor is AntiTorped)
                {
                    float ptzProtection = targetArmor.GetDef(ammo.Type); 
                    float finalDamage = totalBaseDamage * (1 - ptzProtection);
                    return finalDamage;
                }

                return totalBaseDamage * ammo.PenetrationMultiplier; 
            }

            bool isPenetration = gun.PenetratesAllArmor ||
                                 gun.PenetratesArmor == GetArmorType(targetArmor);

            float armorProtection = targetArmor.GetDef(ammo.Type);

            if (isPenetration)
            {
                float damageWithMultiplier = totalBaseDamage * ammo.PenetrationMultiplier;
                return damageWithMultiplier * (1 - armorProtection);
            }
            else
            {
                return totalBaseDamage * (1 - armorProtection);
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
