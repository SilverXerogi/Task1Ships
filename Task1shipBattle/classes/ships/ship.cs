using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Gun Gun { get; private set; }
        public Armor Armor { get; private set; }

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

        public override String ToString()
        {
            return $"{Name} ({Type}) - HP: {CurrentHP:F0}/{MaxHP:F0}, Уклонение: {EvasionChance:P0}";
        }
    }
}                                   
