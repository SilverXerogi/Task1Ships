using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1shipBattle.classes.Equipment;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
    public abstract class Ship
    {
        public String Name { get;}
        public ShipType Type { get;}
        public Single MaxHP { get;}
        public Single CurrentHP { get; private set; }
        public Single EvasionChance { get;}

        public virtual Single RicochetChance => 0f;


        public Inventory Inventory { get; }
        public abstract Int32 MaxInventoryWeight { get; }

        public Gun Gun => Inventory.Guns.FirstOrDefault();
        public Armor Armor => Inventory.Armors.FirstOrDefault();

        public Boolean IsAlive => CurrentHP > 0;

        public abstract Boolean CanEquipTorpedoTube { get; }

        private static readonly Random random = new Random();

        protected Ship(String name, ShipType type, Single maxHP, Single evasionChance)
        {
            Name = name;
            Type = type;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            EvasionChance = evasionChance;
            Inventory = new Inventory(MaxInventoryWeight);
        }


        public void Equip(Gun gun, Armor armor)
        {

            if (gun.Name.Contains("Торпедный") && !CanEquipTorpedoTube)
            {
                throw new InvalidOperationException(
                    $"{Name} ({Type}) не может быть оснащён торпедными аппаратами!");
            }

            Boolean gunAdded = Inventory.AddEquipment(gun);
            Boolean armorAdded = Inventory.AddEquipment(armor);
            if (!gunAdded || !armorAdded)
            {
                throw new InvalidOperationException(
                    $"{Name}: не хватает грузоподъёмности для экипировки! " +
                    $"Вес: {gun.Weight + armor.Weight}, " +
                    $"свободно: {Inventory.MaxWeight - Inventory.CurrentWeight}");
            }

            Console.WriteLine($"{Name} оснащён: {gun.Name} + {armor.Name}");
        }
        public Boolean AddAmmunition(Ammunition ammo)
        {
            return Inventory.AddEquipment(ammo);
        }
        public Boolean TryEvade()
        {
            if (EvasionChance <= 0) return false;

            double roll = random.NextDouble();
            return roll < EvasionChance;
        }

        public Single TakeDamage(Single damage)
        {
            if (!IsAlive) return 0;

            if (TryEvade())
            {
                return 0;
            }

            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;



            return damage;
        }

       
        public void Repair()
        {
            CurrentHP = MaxHP;
        }

        public override String ToString()
        {
            return $"{Name} ({Type}) - HP: {CurrentHP:F0}/{MaxHP:F0}, Уклонение: {EvasionChance:P0}";
        }

        public bool TryRicochet()
        {
            if (RicochetChance <= 0) return false;
            return random.NextDouble() < RicochetChance;
        }

        public Int32 LoadAmmunitionToMax(Ammunition ammo)
        {
            if (Gun == null)
                throw new InvalidOperationException($"{Name}: сначала установи орудие!");

            if (!Gun.CanFireWith(ammo))
                throw new InvalidOperationException(
                    $"{Name}: {ammo.GetName()} несовместим с {Gun.Name}!");

            Int32 loaded = 0;
            
            while (Inventory.AddEquipment(ammo))
            {
                loaded++;
            }

            Console.WriteLine($"{Name}: загружено {loaded} {ammo.GetName()} под завязку " +
                $"(вес: {Inventory.CurrentWeight}/{Inventory.MaxWeight})");
            return loaded;
        }
        public Boolean ConsumeAmmo(AmmoType type)
        {
            var ammo = Inventory.Ammunitions.FirstOrDefault(a => a.Type == type);
            if (ammo == null)
                return false;

            Inventory.RemoveEquipment(ammo);
            return true;
        }
        public Int32 GetAmmoCount(AmmoType type)
        {
            return Inventory.Ammunitions.Count(a => a.Type == type);
        }
    }
}                                   
