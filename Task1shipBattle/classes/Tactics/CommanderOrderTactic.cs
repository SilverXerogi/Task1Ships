using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class CommanderOrderTactic : ITactic
    {
        public string Name => "Приказ командира";

        private static readonly Random random = new Random();

        public Ship SelectTarget(Ship attacker, Squadron enemySquadron, Squadron allySquadron)
        {
            var aliveEnemies = enemySquadron.GetAliveShips();
            if (aliveEnemies.Count == 0) return null;

            Ship target = allySquadron.CommanderTarget;

            if (target == null || !target.IsAlive)
            {
                target = aliveEnemies[random.Next(aliveEnemies.Count)];
            }

            return target;
        }
    }
}
