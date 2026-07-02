using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle.classes.Equipment
{
    public abstract class Equipment
    {
        public Int32 Weight { get; }

        protected Equipment(Int32 weight)
        {
            Weight = weight;
        }
    }
}
