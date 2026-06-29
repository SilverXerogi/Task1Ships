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
        public GunType Type { get; }
        public int ReloadTurns { get; }
        public int CurrentCooldown { get; private set; }
        public bool IsReady => CurrentCooldown <= 0;
        public Ammunition DefaultAmmo { get; private set; }

        public bool PenetratesAllArmor { get; }
        public bool IgnoresArmor { get; } 
        public ArmorType PenetratesArmor { get; } 

        public List<Projectile> InFlightProjectiles { get; } = new List<Projectile>();

        private Gun(
            string name,
            GunType type,
            int reloadTurns,
            Ammunition defaultAmmo,
            bool penetratesAllArmor = false,
            bool ignoresArmor = false,
            ArmorType penetratesArmor = ArmorType.ArmoredBelt)
        {
            Name = name;
            Type = type;
            ReloadTurns = reloadTurns;
            DefaultAmmo = defaultAmmo;
            CurrentCooldown = 0;
            PenetratesAllArmor = penetratesAllArmor;
            IgnoresArmor = ignoresArmor;
            PenetratesArmor = penetratesArmor;
        }

        public static Gun CreateMainGun(Ammunition ammo)
        {
            return new Gun("Башня ГК", GunType.MainGun, 2, ammo, penetratesAllArmor: true);
        }

        public static Gun CreateUniversalGun(Ammunition ammo)
        {
            return new Gun("Универсальное орудие", GunType.Universal, 1, ammo, penetratesArmor: ArmorType.Kazemat);
        }

        public static Gun CreateTorpedoTube(Ammunition ammo)
        {
            return new Gun("Торпедный аппарат", GunType.TorpedoTube, 1, ammo, ignoresArmor: true);
        }
        public bool CanFireWith(Ammunition ammo)
        {
            if (ammo.Type == AmmoType.Torped && Type != GunType.TorpedoTube)
                return false;

            if (Type == GunType.TorpedoTube && ammo.Type != AmmoType.Torped)
                return false;

            return true;
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

            if (!CanFireWith(DefaultAmmo))
            {
                Console.WriteLine($"{Name} не может стрелять снарядом {DefaultAmmo.GetName()}!");
                return false;
            }

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
