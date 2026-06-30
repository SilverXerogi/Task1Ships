using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class FinishOffTactic : ITactic
    {
        public string Name => "Добивание";

        public Ship SelectTarget(Ship attacker, Squadron enemySquadron, Squadron allySquadron)
        {
            var aliveEnemies = enemySquadron.GetAliveShips();
            if (aliveEnemies.Count == 0) return null;

            return aliveEnemies.OrderBy(s => s.CurrentHP).First();
        }
    }
}
