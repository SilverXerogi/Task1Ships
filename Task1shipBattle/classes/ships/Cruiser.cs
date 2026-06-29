using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle
{
    public class Cruiser : Ship
    {

        public bool HasTorpedoTubes { get; }

        public override bool CanEquipTorpedoTube => HasTorpedoTubes;

        public Cruiser(string name = "Крейсер", bool hasTorpedoTubes = false): base(name, ShipType.Cruiser, maxHP: 580f, evasionChance: 0.07f)
        {
            HasTorpedoTubes = hasTorpedoTubes;
        }
    }
}
