

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
            Console.WriteLine($"Команда '{Name}'");
                foreach (var  ship in Ships)
            {
                String status = ship.IsAlive ? "Живой" : "Мертвый";
                Console.WriteLine($"{ship} {status}");
            }
        }


    }
}
