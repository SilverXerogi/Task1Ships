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

            var red = new Squadron("Красная эскадра");
            var blue = new Squadron("Синяя эскадра");

            red.AddShip(new Destroyer("Эсминец 'Грозный'"));
            red.AddShip(new Cruiser("Крейсер 'Варяг'", hasTorpedoTubes: true));
            red.AddShip(new Battleship("Линкор 'Полтава'"));

            blue.AddShip(new Destroyer("Эсминец 'Быстрый'"));
            blue.AddShip(new Cruiser("Крейсер 'Аврора'", hasTorpedoTubes: false));
            blue.AddShip(new Battleship("Линкор 'Севастополь'"));

            var warehouse = new Warehouse();
            warehouse.EquipSquadronSmart(red);
            warehouse.EquipSquadronSmart(blue);

            red.SetTactic(new ConcentrationTactic());
            blue.SetTactic(new PriorityTypeTactic());

            var battle = new Battle(red, blue);
            battle.Start();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}