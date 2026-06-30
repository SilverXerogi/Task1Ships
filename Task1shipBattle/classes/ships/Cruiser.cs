using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
    public class Cruiser : Ship
    {

        public Boolean HasTorpedoTubes { get; }

        public override Boolean CanEquipTorpedoTube => HasTorpedoTubes;

        public Cruiser(String name = "Крейсер", Boolean hasTorpedoTubes = false): base(name, ShipType.Cruiser, maxHP: 580f, evasionChance: 0.07f)
        {
            HasTorpedoTubes = hasTorpedoTubes;
        }
    }
}
