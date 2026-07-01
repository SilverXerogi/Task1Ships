using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1shipBattle.classes.ships;


namespace Task1shipBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            int numSquadrons = 0;
            while ((numSquadrons >= 6)^(numSquadrons<=2))
            {
                Console.Write("Введите количество команд (2-6): ");
                numSquadrons = int.Parse(Console.ReadLine());
            }
            string[] names = { "Красная", "Синяя", "Зелёная", "Жёлтая", "Фиолетовая", "Оранжевая" };
            var squadrons = new List<Squadron>();
            var random = new Random();

            for (int i = 0; i < numSquadrons; i++)
            {
                var squadron = new Squadron(names[i]);
                squadron.AddShip(new Destroyer($"Эсминец '{names[i]}'"));
                squadron.AddShip(new Cruiser($"Крейсер '{names[i]}'", hasTorpedoTubes: random.Next(2) == 0));
                squadron.AddShip(new Battleship($"Линкор '{names[i]}'"));
                squadrons.Add(squadron);
            }

            var warehouse = new Warehouse();
            foreach (var squadron in squadrons)
            {
                warehouse.EquipSquadronSmart(squadron);
                squadron.SetRandomTactic();
            }

            var battle = new Battle(squadrons.ToArray());
            battle.Start();

        }
    }
}