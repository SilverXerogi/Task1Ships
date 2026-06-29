using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1shipBattle.classes.ships;
using Task1shipBattle.classes.weapons;

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


            Console.WriteLine("\n=== ЭТАП 2: Тест совместимости ===\n");

            

            // Создаем броню
            Armor ptz = new antiTorped();
            Armor belt = new ArmoredBelt();

            Console.WriteLine("--- Тест 1: Бронебойный vs ПТЗ (гасится) ---");
            float dmg1 = DamageCalculator.CalculateHit(mainGun, apShell, ptz);
            Console.WriteLine($"ГК (бронебойный) vs ПТЗ: {dmg1} урона (ожидается: 0)");

            Console.WriteLine("\n--- Тест 2: Бронебойный vs Пояс ---");
            float dmg2 = DamageCalculator.CalculateHit(mainGun, apShell, belt);
            Console.WriteLine($"ГК (бронебойный) vs Пояс: {dmg2} урона");

            Console.WriteLine("\n--- Тест 3: Торпеда из универсального орудия (запрещено) ---");
            bool canFire = universalGun.CanFireWith(torpedo);
            Console.WriteLine($"Универсальное орудие может стрелять торпедой: {canFire} (ожидается: False)");

            Console.WriteLine("\n--- Тест 4: Торпеда из торпедного аппарата ---");
            bool canFire2 = torpedoTube.CanFireWith(torpedo);
            Console.WriteLine($"Торпедный аппарат может стрелять торпедой: {canFire2} (ожидается: True)");

            Console.WriteLine("\n--- Тест 5: Бронебойный из торпедного аппарата (запрещено) ---");
            bool canFire3 = torpedoTube.CanFireWith(apShell);
            Console.WriteLine($"Торпедный аппарат может стрелять бронебойным: {canFire3} (ожидается: False)");

            Console.WriteLine("\n--- Тест 6: Торпеда vs ПТЗ (двойной урон) ---");
            float dmg6 = DamageCalculator.CalculateHit(torpedoTube, torpedo, ptz);
            Console.WriteLine($"Торпеда vs ПТЗ: {dmg6} урона (ожидается: 40 = 20*2)");

            Console.WriteLine("\n--- Тест 7: Торпеда vs Пояс (игнорирует броню) ---");
            float dmg7 = DamageCalculator.CalculateHit(torpedoTube, torpedo, belt);
            Console.WriteLine($"Торпеда vs Пояс: {dmg7} урона (ожидается: 20)");
        }
    }
 
}
