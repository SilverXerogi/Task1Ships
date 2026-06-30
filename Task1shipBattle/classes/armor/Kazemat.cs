using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;
using static Task1shipBattle.Armor;


namespace Task1shipBattle
{
    public class Kazemat : Armor
    {
        public Kazemat() : base("Казематная броня")
        {

        }
        public override Single GetDef(AmmoType ammoType)
        {
            return 0.25f;
        }
    }
}
