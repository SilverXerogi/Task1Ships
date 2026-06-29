using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle
{
   
    public abstract class Armor
    {
        public string Name { get; set; }
             
        public Armor(string Name)
        {
            this.Name = Name;
            
        }

        public abstract float GetDef(AmmoType ammoType);

        public float CalcDamage(float rawDamage, AmmoType ammoType)
        {
            float def = GetDef(ammoType);
            float finDamage = rawDamage * (1 - def);
            return (float)Math.Round(finDamage, (2));
        }
    }

    

    
}
