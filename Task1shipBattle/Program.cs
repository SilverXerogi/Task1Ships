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

            var red = new Squadron("Красная");
            var blue = new Squadron("Синяя");
            var green = new Squadron("Зелёная");

            // Красные
            red.AddShip(new Destroyer("Эсминец 'Грозный'"));
            red.AddShip(new Cruiser("Крейсер 'Варяг'", hasTorpedoTubes: true));
            red.AddShip(new Battleship("Линкор 'Полтава'"));

            // Синие
            blue.AddShip(new Destroyer("Эсминец 'Быстрый'"));
            blue.AddShip(new Cruiser("Крейсер 'Аврора'", hasTorpedoTubes: false));
            blue.AddShip(new Battleship("Линкор 'Севастополь'"));

            // Зелёные
            green.AddShip(new Destroyer("Эсминец 'Смерч'"));
            green.AddShip(new Cruiser("Крейсер 'Богатырь'", hasTorpedoTubes: true));
            green.AddShip(new Battleship("Линкор 'Император'"));

            var warehouse = new Warehouse();
            warehouse.EquipSquadronSmart(red);
            warehouse.EquipSquadronSmart(blue);
            warehouse.EquipSquadronSmart(green);

            red.SetTactic(new ConcentrationTactic());
            blue.SetTactic(new PriorityTypeTactic());
            green.SetTactic(new HuntLeaderTactic());

            var battle = new Battle(red, blue, green);
            battle.Start();

        }
    }
}