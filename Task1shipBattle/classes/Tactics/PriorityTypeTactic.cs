using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
    public class PriorityTypeTactic : ITactic
    {
        public string Name => "По приоритету типов";

        private static readonly Random random = new Random();

        public Ship SelectTarget(Ship attacker, Squadron enemySquadron, Squadron allySquadron)
        {
            var aliveEnemies = enemySquadron.GetAliveShips();
            if (aliveEnemies.Count == 0) return null;

            ShipType priorityTargetType = GetPriorityTargetType(attacker.Type);

            var priorityTargets = aliveEnemies.Where(s => s.Type == priorityTargetType).ToList();

            if (priorityTargets.Count > 0)
            {
                return priorityTargets[random.Next(priorityTargets.Count)];
            }

            return aliveEnemies[random.Next(aliveEnemies.Count)];
        }

        private ShipType GetPriorityTargetType(ShipType attackerType)
        {
            switch (attackerType)
            {
                case ShipType.Destroyer:
                    return ShipType.Battleship;   // Эсминец - Линкор
                case ShipType.Cruiser:
                    return ShipType.Destroyer;    // Крейсер - Эсминец
                case ShipType.Battleship:
                    return ShipType.Cruiser;      // Линкор - Крейсер
                default:
                    return ShipType.Destroyer;
            }
        }
    }
}
