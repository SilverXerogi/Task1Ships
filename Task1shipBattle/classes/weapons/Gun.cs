using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle
{
    public class Gun
    {
        public string Name { get; }
        public int ReloadTurns { get; }
        public int CurrentCooldown { get; private set; }
        public bool IsReady => CurrentCooldown <= 0;
        public Ammunition DefaultAmmo { get; private set; }

        // Характеристики пробития орудия
        public bool PenetratesAllArmor { get; }  // Пробивает любую броню (ГК)
        public bool IgnoresArmor { get; }  // Игнорирует броню (торпеды)
        public ArmorType PenetratesArmor { get; }  // Какую броню пробивает (универсальные)

        public List<Projectile> InFlightProjectiles { get; } = new List<Projectile>();

        private Gun(
            string name,
            int reloadTurns,
            Ammunition defaultAmmo,
            bool penetratesAllArmor = false,
            bool ignoresArmor = false,
            ArmorType penetratesArmor = ArmorType.ArmoredBelt)
        {
            Name = name;
            ReloadTurns = reloadTurns;
            DefaultAmmo = defaultAmmo;
            CurrentCooldown = 0;
            PenetratesAllArmor = penetratesAllArmor;
            IgnoresArmor = ignoresArmor;
            PenetratesArmor = penetratesArmor;
        }

        // Фабричные методы для создания орудий
        public static Gun CreateMainGun(Ammunition ammo)
        {
            return new Gun("Башня ГК", 2, ammo, penetratesAllArmor: true);
        }

        public static Gun CreateUniversalGun(Ammunition ammo)
        {
            return new Gun("Универсальное орудие", 1, ammo, penetratesArmor: ArmorType.Kazemat);
        }

        public static Gun CreateTorpedoTube(Ammunition ammo)
        {
            return new Gun("Торпедный аппарат", 1, ammo, ignoresArmor: true);
        }

        public void EndTurn()
        {
            if (CurrentCooldown > 0)
                CurrentCooldown--;

            foreach (var projectile in InFlightProjectiles)
            {
                projectile.UpdateFlight();
            }
        }

        public List<Projectile> GetArrivedProjectiles()
        {
            var arrived = InFlightProjectiles.FindAll(p => p.HasArrived);
            InFlightProjectiles.RemoveAll(p => p.HasArrived);
            return arrived;
        }

        public bool TryFire(Armor targetArmor, out Projectile firedProjectile)
        {
            firedProjectile = null;
            if (!IsReady)
                return false;

            CurrentCooldown = ReloadTurns;

            if (DefaultAmmo.FlightTurns > 0)
            {
                firedProjectile = new Projectile(DefaultAmmo, targetArmor, DefaultAmmo.FlightTurns);
                InFlightProjectiles.Add(firedProjectile);
            }
            else
            {
                firedProjectile = new Projectile(DefaultAmmo, targetArmor, 0);
            }

            return true;
        }
    }
}
