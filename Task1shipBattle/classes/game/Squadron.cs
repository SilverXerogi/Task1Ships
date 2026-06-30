

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

        public void SetCommanderTarget(Ship target)
        {
            CommanderTarget = target;
            if (target != null)
                Console.WriteLine($"Командир '{Name}' назначил цель: {target.Name}");
        }


    }
}
