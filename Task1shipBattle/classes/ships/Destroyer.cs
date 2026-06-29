using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle
{
    public class Destroyer : Ship
    {
        public override bool CanEquipTorpedoTube => true;

        public Destroyer(string name = "Эсминец"): base(name, ShipType.Destroyer, maxHP: 400f, evasionChance: 0.15f){ }
    }
}
