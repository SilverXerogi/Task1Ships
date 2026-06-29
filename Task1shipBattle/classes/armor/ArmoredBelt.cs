using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;
using static Task1shipBattle.Armor;

namespace Task1shipBattle
{
    public class ArmoredBelt : Armor
    {
        public ArmoredBelt() : base("Броневой пояс по ватерлинии")
        {

        }
        public override float GetDef(AmmoType ammoType)
        {
            return 0.3f;
        }
    }
}
