using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class enums
    {
        public enum AmmoType
        {
            AP,
            HE,
            Torped
        }

        public enum ArmorType
        {
            ArmoredBelt,
            Kazemat,
            antiTorped
        }
        public enum ShipType
        {
            Destroyer, 
            Cruiser,     
            Battleship   
        }
        public enum GunType
        {
            MainGun,
            Universal,
            TorpedoTube
        }
    }
}
