using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Task1shipBattle.enums;

namespace Task1shipBattle
{
    public abstract class Ship
    {
        public string Name { get;  set; }
        public ShipType Type { get;  set; }
        public float MaxHP { get;  set; }
        public float CurrentHP { get;  set; }
        public float EvasionChance { get;  set; } 

        public Gun Gun { get;  set; }
        public Armor Armor { get;  set; }

        public bool IsAlive => CurrentHP > 0;

        public abstract bool CanEquipTorpedoTube { get; }

        private static readonly Random random = new Random();

        protected Ship(string name, ShipType type, float maxHP, float evasionChance)
        {
            Name = name;
            Type = type;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            EvasionChance = evasionChance;
        }


        public void Equip(Gun gun, Armor armor)
        {

            if (gun.Name.Contains("Торпедный") && !CanEquipTorpedoTube)
            {
                throw new InvalidOperationException(
                    $"{Name} ({Type}) не может быть оснащён торпедными аппаратами!");
            }

            Gun = gun;
            Armor = armor;
            Console.WriteLine($"{Name} оснащён: {gun.Name} + {armor.Name}");
        }

        public bool TryEvade()
        {
            if (EvasionChance <= 0) return false;

            double roll = random.NextDouble();
            return roll < EvasionChance;
        }

        public float TakeDamage(float damage)
        {
            if (!IsAlive) return 0;

            if (TryEvade())
            {
                Console.WriteLine($"{Name} уклонился от атаки");
                return 0;
            }

            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;

            Console.WriteLine($"{Name} получил {damage:F1} урона. HP: {CurrentHP:F1}/{MaxHP}");

            if (!IsAlive)
            {
                Console.WriteLine($"{Name} УНИЧТОЖЕН!");
            }

            return damage;
        }

       
        public void Repair()
        {
            CurrentHP = MaxHP;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - HP: {CurrentHP:F0}/{MaxHP:F0}, Уклонение: {EvasionChance:P0}";
        }
    }
}
