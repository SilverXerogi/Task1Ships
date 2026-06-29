using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle.classes.ships
{
    public class Battleship : Ship
    {
        public override bool CanEquipTorpedoTube => false;

        public Battleship(string name = "Линкор"): base(name, ShipType.Battleship, maxHP: 820f, evasionChance: 0f){ }
    }
}
