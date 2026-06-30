using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
   
    public abstract class Armor
    {
        public String Name { get;}
             
        public Armor(String Name)
        {
            this.Name = Name;
            
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
