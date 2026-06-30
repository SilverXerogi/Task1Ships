

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class Squadron
    {
        public String Name { get; }
        public List<Ship> Ships { get; } = new List<Ship>();

        public ITactic Tactic { get; set; }
        public Ship CommanderTarget { get; set; }

        private static readonly Random random = new Random();

        private static readonly ITactic[] allTactics = new ITactic[]
        {
            new CommanderOrderTactic(),
            new HuntLeaderTactic(),
            new FinishOffTactic(),
            new ConcentrationTactic(),
            new PriorityTypeTactic()
        };

        public Squadron(String name) 
        {
            Name = name;
        }
        public void AddShip(Ship ship)
        {
            Ships.Add(ship);
            Console.WriteLine($"Корабль {ship.Name} добавлен в команду '{Name}'");
        }
        public Boolean HasAliveShips()
        {
            return Ships.Any(s => s.IsAlive);
        }

        public List<Ship> GetAliveShips()
        {
            return Ships.Where(s => s.IsAlive).ToList();
        }

        public void PrintStatus()
        {
            Console.WriteLine($"\nКоманда '{Name}'");
                foreach (var  ship in Ships)
            {
                String status = ship.IsAlive ? "Живой" : "Мертвый";
                Console.WriteLine($"{ship} {status}");
            }
        }
        public void SetTactic(ITactic tactic)
        {
            Tactic = tactic;
            Console.WriteLine($"Эскадра '{Name}' использует тактику: {tactic.Name}");
        }
        public void SetRandomTactic()
        {
            int index = random.Next(allTactics.Length);
            Tactic = allTactics[index];
        }
        public void SetCommanderTarget(Ship target)
        {
            CommanderTarget = target;
            if (target != null)
                Console.WriteLine($"Командир '{Name}' назначил цель: {target.Name}");
        }


    }
}
