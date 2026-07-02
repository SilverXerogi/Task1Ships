using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
    public class Destroyer : Ship
    {
        public override Boolean CanEquipTorpedoTube => true;

        public override Int32 MaxInventoryWeight => 800;

        public Destroyer(String name = "Эсминец"): base(name, ShipType.Destroyer, maxHP: 400f, evasionChance: 0.15f){ }
    }
}
