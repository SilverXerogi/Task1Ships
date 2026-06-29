using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;
using static Task1shipBattle.Armor;

namespace Task1shipBattle
{
    public class antiTorped : Armor
    {
        public antiTorped() : base("Противоторпедная защита")
        {

        }
        public override float GetDef(AmmoType ammoType)
        {
            if (ammoType == AmmoType.Torped)
            {
                return 0.5f;
            }
            return 0.15f;
        }
    }
}
