using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1shipBattle.classes.Equipment;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
   
    public abstract class Armor : Equipment
    {
        public String Name { get;}
             
        public Armor(string name, Int32 weight) : base(weight)
        {
            this.Name = name;
            
        }

        public abstract Single GetDef(AmmoType ammoType);

        public Single CalcDamage(Single rawDamage, AmmoType ammoType)
        {
            Single def = GetDef(ammoType);
            Single finDamage = rawDamage * (1 - def);
            return (Single)Math.Round(finDamage, (2));
        }
    }

    

    
}
