using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
    public class Gun
    {
        public String Name { get; }
        public GunType Type { get; }
        public Int32 ReloadTurns { get; }
        public Int32 CurrentCooldown { get; private set; }
        public Boolean IsReady => CurrentCooldown <= 0;
        public Ammunition DefaultAmmo { get; private set; }

        public Single MinDamage { get;}
        public Single MaxDamage { get;}

        public Boolean PenetratesAllArmor { get; }
        public Boolean IgnoresArmor { get; } 
        public ArmorType PenetratesArmor { get; } 

        public List<Projectile> InFlightProjectiles { get; } = new List<Projectile>();

        private Gun(
            String name,
            GunType type,
            Int32 reloadTurns,
            Ammunition defaultAmmo,
            Single minDamage, 
            Single maxDamage,
            Boolean penetratesAllArmor = false,
            Boolean ignoresArmor = false,
            ArmorType penetratesArmor = ArmorType.ArmoredBelt)
        {
            Name = name;
            Type = type;
            ReloadTurns = reloadTurns;
            DefaultAmmo = defaultAmmo;
            MinDamage = minDamage; 
            MaxDamage = maxDamage;
            CurrentCooldown = 0;
            PenetratesAllArmor = penetratesAllArmor;
            IgnoresArmor = ignoresArmor;
            PenetratesArmor = penetratesArmor;
        }
        public Single GetRandomDamage()
        {
            return (Single)(Random.NextDouble()*(MaxDamage - MinDamage) + MinDamage;
        }
        public static Gun CreateMainGun(Ammunition ammo)
        {
            return new Gun("Башня ГК", GunType.MainGun, 2, 
                minDamage:40f, maxDamage:50f, 
                ammo penetratesAllArmor: true);
        }

        public static Gun CreateUniversalGun(Ammunition ammo)
        {
            return new Gun("Универсальное орудие", GunType.Universal, 1,
                minDamage: 20f, maxDamage: 30f,
                ammo, penetratesArmor: ArmorType.Kazemat);
        }

        public static Gun CreateTorpedoTube(Ammunition ammo)
        {
            return new Gun("Торпедный аппарат", GunType.TorpedoTube, 1,
                minDamage: 35f, maxDamage: 45f,
                ammo, ignoresArmor: true);
        }
        public Boolean CanFireWith(Ammunition ammo)
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

        public Boolean TryFire(Armor targetArmor, out Projectile firedProjectile)
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
