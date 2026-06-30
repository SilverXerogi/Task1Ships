using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1shipBattle
{
    public class BattleLogger
    {
        private readonly Dictionary<string, ConsoleColor> squadronColors = new Dictionary<string, ConsoleColor>();

        private readonly ConsoleColor[] colorPalette = new ConsoleColor[]
        {
            ConsoleColor.Red,
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.Yellow,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkYellow
        };

        private int colorIndex = 0;

        public BattleLogger(IEnumerable<Squadron> squadrons)
        {
            foreach (var squadron in squadrons)
            {
                squadronColors[squadron.Name] = colorPalette[colorIndex % colorPalette.Length];
                colorIndex++;
            }
        }

        public ConsoleColor GetColor(Squadron squadron)
        {
            return squadronColors.TryGetValue(squadron.Name, out var color)
                ? color
                : ConsoleColor.White;
        }

        private void WriteSquadron(Squadron squadron, string text)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = GetColor(squadron);
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        private string GetSquadronTag(Squadron squadron)
        {
            string tag = squadron.Name.Length >= 25
                ? squadron.Name.Substring(0, 25)
                : squadron.Name;
            return tag.ToUpper();
        }

        public void LogShot(Ship attacker, Squadron attackerSquadron, Gun gun,
                           Ship target, Squadron targetSquadron, float damage)
        {
            WriteSquadron(attackerSquadron, $"[{GetSquadronTag(attackerSquadron)}] ");
            Console.Write($"{attacker.Name} стреляет из {gun.Name} - ");
            WriteSquadron(targetSquadron, target.Name);
            Console.WriteLine($" получил {damage:F1} урона (HP: {target.CurrentHP:F0}/{target.MaxHP:F0})");
        }

        public void LogTorpedoLaunch(Ship attacker, Squadron attackerSquadron, Gun gun,
                                     Ship target, Squadron targetSquadron, int flightTurns)
        {
            WriteSquadron(attackerSquadron, $"[{GetSquadronTag(attackerSquadron)}] ");
            Console.Write($"{attacker.Name} стреляет из {gun.Name} - Торпеда летит в ");
            WriteSquadron(targetSquadron, target.Name);
            Console.WriteLine($" (долетит через {flightTurns} ход)");
        }

        public void LogTorpedoHit(Ship attacker, Squadron attackerSquadron,
                                  Ship target, Squadron targetSquadron, float damage)
        {
            WriteSquadron(attackerSquadron, $"[{GetSquadronTag(attackerSquadron)}] ");
            Console.Write($"Торпеда от {attacker.Name} долетела - ");
            WriteSquadron(targetSquadron, target.Name);
            Console.WriteLine($" получил {damage:F1} урона (HP: {target.CurrentHP:F0}/{target.MaxHP:F0})");
        }

        public void LogEvade(Ship target, Squadron targetSquadron)
        {
            Console.Write("   ");
            WriteSquadron(targetSquadron, target.Name);
            Console.WriteLine(" уклонился от атаки");
        }

        public void LogDeath(Ship ship, Squadron squadron)
        {
            Console.Write("   ");
            WriteSquadron(squadron, ship.Name);
            Console.WriteLine(" УНИЧТОЖЕН!");
        }

        public void LogRicochet(Ship source, Squadron sourceSquadron,
                                Ship target, Squadron targetSquadron, float damage)
        {
            Console.Write("   Рикошет от ");
            WriteSquadron(sourceSquadron, source.Name);
            Console.Write(" - ");
            WriteSquadron(targetSquadron, target.Name);
            Console.WriteLine($" получил {damage:F1} урона (HP: {target.CurrentHP:F0}/{target.MaxHP:F0})");
        }

        public void LogReloading(Ship ship, Squadron squadron, int cooldown, int maxCooldown)
        {
            WriteSquadron(squadron, $"[{GetSquadronTag(squadron)}] ");
            Console.WriteLine($"{ship.Name} - перезарядка ({cooldown}/{maxCooldown})");
        }

        public void LogMiss(Ship attacker, Squadron attackerSquadron, Ship target, Squadron targetSquadron)
        {
            Console.Write("   Снаряд от ");
            WriteSquadron(attackerSquadron, attacker.Name);
            Console.Write(" пролетел мимо - ");
            WriteSquadron(targetSquadron, target.Name);
            Console.WriteLine(" уже уничтожен");
        }

        public void PrintSquadronStatus(Squadron squadron)
        {
            int alive = squadron.GetAliveShips().Count;
            int total = squadron.Ships.Count;

            WriteSquadron(squadron, $"  {squadron.Name} [{alive}/{total}]");
            Console.WriteLine($" (тактика: {squadron.Tactic?.Name ?? "Нет"})");

            foreach (var ship in squadron.Ships)
            {
                WriteSquadron(squadron, $"     {ship.Name} ({ship.Type}) ");

                if (ship.IsAlive)
                {
                    Console.Write($"HP: {ship.CurrentHP:F0}/{ship.MaxHP:F0}");

                    if (ship.Gun != null && !ship.Gun.IsReady)
                    {
                        Console.Write($" [перезарядка {ship.Gun.CurrentCooldown}/{ship.Gun.ReloadTurns}]");
                    }
                }
                else
                {
                    Console.Write("[УНИЧТОЖЕН]");
                }
                Console.WriteLine();
            }
        }
    }
}
