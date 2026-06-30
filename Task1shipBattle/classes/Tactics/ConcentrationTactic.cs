using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class ConcentrationTactic : ITactic
    {
        public string Name => "Концентрация";

        private Ship currentTarget;

        private static readonly Random random = new Random();

        public Ship SelectTarget(Ship attacker, Squadron enemySquadron, Squadron allySquadron)
        {
            var aliveEnemies = enemySquadron.GetAliveShips();
            if (aliveEnemies.Count == 0)
            {
                currentTarget = null;
                return null;
            }

            if (currentTarget == null || !currentTarget.IsAlive)
            {
                currentTarget = aliveEnemies[random.Next(aliveEnemies.Count)];
            }

            return currentTarget;
        }
    }
}
