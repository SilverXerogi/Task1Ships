using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;


namespace Task1shipBattle
{
    public class Warehouse
    {
        private static readonly Random random = new Random();

        private List<Ammunition> availableAmmo = new List<Ammunition>();
        private List<Armor> availableArmor = new List<Armor>();

        public Warehouse()
        {
            availableAmmo.Add(Ammunition.CreateArmorPiercing());
            availableAmmo.Add(Ammunition.CreateHighExplosive());
            availableAmmo.Add(Ammunition.CreateTorpedo());

            availableArmor.Add(new ArmoredBelt());
            availableArmor.Add(new Kazemat());
            availableArmor.Add(new AntiTorped());
        }

        public Ammunition GetRandomAmmoForGun(Gun gun)
        {
            var compatibleAmmo = availableAmmo.FindAll(a => gun.CanFireWith(a));

            if (compatibleAmmo.Count == 0)
                throw new InvalidOperationException($"Нет совместимых снарядов для {gun.Name}");

            return compatibleAmmo[random.Next(compatibleAmmo.Count)];
        }

        public Armor GetRandomArmor()
        {
            return availableArmor[random.Next(availableArmor.Count)];
        }

        public Gun GetRandomGun(Ship ship)
        {
            var availableGuns = new List<Func<Ammunition, Gun>>();

            availableGuns.Add(Gun.CreateMainGun);

            availableGuns.Add(Gun.CreateUniversalGun);

            if (ship.CanEquipTorpedoTube)
            {
                availableGuns.Add(Gun.CreateTorpedoTube);
            }

            var gunFactory = availableGuns[random.Next(availableGuns.Count)];

            var tempAmmo = Ammunition.CreateArmorPiercing();
            var tempGun = gunFactory(tempAmmo);

            var ammo = GetRandomAmmoForGun(tempGun);

            return gunFactory(ammo);
        }

        public void EquipSquadron(Squadron squadron)
        {
            Console.WriteLine($"\n=== Снаряжение эскадры '{squadron.Name}' ===");

            foreach (var ship in squadron.Ships)
            {
                try
                {
                    var gun = GetRandomGun(ship);
                    var armor = GetRandomArmor();
                    ship.Equip(gun, armor);

                    var ammo = GetRandomAmmoForGun(gun);
                    ship.LoadAmmunitionToMax(ammo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Ошибка при снаряжении {ship.Name}: {ex.Message}");
                }
            }
        }

        public void EquipSquadronSmart(Squadron squadron)
        {
            Console.WriteLine($"\n=== Умное снаряжение эскадры '{squadron.Name}' ===");

            foreach (var ship in squadron.Ships)
            {
                Gun gun = null;
                Armor armor = null;
                Ammunition ammo = null;
                switch (ship.Type)
                {
                    case ShipType.Destroyer:
                        
                        gun = Gun.CreateTorpedoTube(Ammunition.CreateTorpedo());
                        armor = new AntiTorped();
                        ammo = Ammunition.CreateTorpedo();
                        break;

                    case ShipType.Cruiser:
                        var cruiser = ship as Cruiser;
                        if (cruiser.HasTorpedoTubes)
                        {
                            gun = Gun.CreateUniversalGun(Ammunition.CreateHighExplosive());
                            armor = new Kazemat();
                            ammo = Ammunition.CreateHighExplosive();
                        }
                        else
                        {
                            gun = Gun.CreateMainGun(Ammunition.CreateArmorPiercing());
                            armor = new ArmoredBelt();
                            ammo = Ammunition.CreateArmorPiercing();
                        }
                        break;

                    case ShipType.Battleship:
                      
                        gun = Gun.CreateMainGun(Ammunition.CreateArmorPiercing());
                        armor = new ArmoredBelt();
                        ammo = Ammunition.CreateArmorPiercing();
                        break;
                }

                try
                {
                    ship.Equip(gun, armor);
                    ship.LoadAmmunitionToMax(ammo);
                    for (int i = 0; i < 3; i++)
                    {
                        if (!ship.AddAmmunition(gun.DefaultAmmo))
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при снаряжении {ship.Name}: {ex.Message}");
                }
            }
        }
    }
}
