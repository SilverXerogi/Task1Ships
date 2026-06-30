using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public interface ITactic
    {
        
        string Name { get; }
        /// attacker Атакующий корабль
        /// enemySquadron Вражеская эскадра
        /// allySquadron Своя эскадра (нужна для приказа командира)
        Ship SelectTarget(Ship attacker, Squadron enemySquadron, Squadron allySquadron);
    }
}