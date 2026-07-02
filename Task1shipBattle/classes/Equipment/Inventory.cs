using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle.classes.Equipment
{
    public class Inventory
    {
        private readonly List<Equipment> _equipments = new List<Equipment>();
        public Equipment[] Equipments => _equipments.ToArray();

        public Int32 MaxWeight { get; }
        public Int32 CurrentWeight => Equipments.Sum(e => e.Weight);

        public Gun[] Guns => Equipments.OfType<Gun>().ToArray();
        public Armor[] Armors => Equipments.OfType<Armor>().ToArray();
        public Ammunition[] Ammunitions => Equipments.OfType<Ammunition>().ToArray();

        public Inventory(Int32 maxWeight)
        {
            MaxWeight = maxWeight;
        }

        public Boolean AddEquipment(Equipment equipment)
        {
            if (CurrentWeight + equipment.Weight > MaxWeight)
                return false;

            _equipments.Add(equipment);
            return true;
        }

        public void RemoveEquipment(Equipment equipment)
        {
            _equipments.Remove(equipment);
        }
    }
}
