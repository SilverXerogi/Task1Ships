using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1shipBattle.classes.ships;

namespace Task1shipBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ЭТАП 1: Тест кораблей\n");

            var destroyer = new Destroyer("Эсминец ");
            var cruiser = new Cruiser("Крейсер ", hasTorpedoTubes: true);
            var cruiserNoTorpedo = new Cruiser("Крейсер ", hasTorpedoTubes: false);
            var battleship = new Battleship("Линкор ");

            Console.WriteLine("Созданные корабли");
            Console.WriteLine(destroyer);
            Console.WriteLine(cruiser);
            Console.WriteLine(cruiserNoTorpedo);
            Console.WriteLine(battleship);

            var apShell = Ammunition.CreateArmorPiercing();
            var heShell = Ammunition.CreateHighExplosive();
            var torpedo = Ammunition.CreateTorpedo();

            var mainGun = Gun.CreateMainGun(apShell);
            var universalGun = Gun.CreateUniversalGun(heShell);
            var torpedoTube = Gun.CreateTorpedoTube(torpedo);

            var beltArmor = new ArmoredBelt();
            var casemateArmor = new Kazemat();

            Console.WriteLine("\nЭкипировка кораблей");

            destroyer.Equip(torpedoTube, beltArmor);

            cruiser.Equip(torpedoTube, casemateArmor);
            cruiserNoTorpedo.Equip(universalGun, beltArmor);
            battleship.Equip(mainGun, beltArmor);
           

            Console.WriteLine("\nТест урона и уклонения");

            Console.WriteLine($"\n{destroyer.Name} (уклонение 15%):");
            for (int i = 0; i < 5; i++)
            {
                destroyer.TakeDamage(50);
            }

            Console.WriteLine($"\n{battleship.Name} (уклонение 0%):");
            battleship.TakeDamage(100);
            battleship.TakeDamage(100);

            Console.WriteLine("\nФинальное состояние");
            Console.WriteLine(destroyer);
            Console.WriteLine(cruiser);
            Console.WriteLine(cruiserNoTorpedo);
            Console.WriteLine(battleship);
        }
    }
 
}
