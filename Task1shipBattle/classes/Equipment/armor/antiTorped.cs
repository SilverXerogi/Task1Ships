using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;
using static Task1shipBattle.Armor;

namespace Task1shipBattle
{
    public class AntiTorped : Armor
    {
        public AntiTorped() : base("Противоторпедная защита", weight: 25)
        {

        }
        public override Single GetDef(AmmoType ammoType)
        {
            if (ammoType == AmmoType.Torped)
            {
                return 0.5f;
            }
            return 0.15f;
        }
    }
}
