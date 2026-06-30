using System;
using System.Collections.Generic;
using System.Linq;
using Task1shipBattle.classes.weapons;
using static Task1shipBattle.Enums;

namespace Task1shipBattle
{
    public class Battle
    {
        private readonly List<Squadron> squadrons;
        private readonly Dictionary<Ship, Squadron> shipToSquadron = new Dictionary<Ship, Squadron>();
        private int turnCount;
        private static readonly Random random = new Random();
        private readonly List<Projectile> globalProjectiles = new List<Projectile>();
        private readonly BattleLogger logger;

        public Battle(params Squadron[] squadrons)
        {
            this.squadrons = squadrons.ToList();
            turnCount = 0;

            foreach (var squadron in squadrons)
            {
                foreach (var ship in squadron.Ships)
                {
                    shipToSquadron[ship] = squadron;
                }
            }

            logger = new BattleLogger(squadrons);
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          НАЧАЛО МОРСКОГО БОЯ         ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");
            Console.ResetColor();

            foreach (var squadron in squadrons)
            {
                PrintSquadronInfo(squadron);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("─".PadRight(40, '─'));
            Console.ResetColor();

            while (!IsBattleOver())
            {
                turnCount++;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n{new string('═', 40)}");
                Console.WriteLine($"          ХОД {turnCount}");
                Console.WriteLine(new string('═', 40));
                Console.ResetColor();

                ExecuteTurn();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nСтатус после хода {turnCount}:");
                Console.ResetColor();
                foreach (var squadron in squadrons)
                {
                    logger.PrintSquadronStatus(squadron);
                }

                if (IsBattleOver()) break;
            }

            PrintBattleResults();
        }

        private void ExecuteTurn()
        {
            Console.WriteLine("\nФАЗА СТРЕЛЬБЫ:");

            var allShots = new List<Projectile>();

            var shuffledSquadrons = squadrons.OrderBy(x => random.Next()).ToList();

            foreach (var squadron in shuffledSquadrons)
            {
                if (!squadron.HasAliveShips()) continue;

                var enemySquadrons = squadrons.Where(s => s != squadron && s.HasAliveShips()).ToList();
                if (enemySquadrons.Count == 0) continue;

                foreach (var ship in squadron.GetAliveShips())
                {
                    if (ship.Gun == null || ship.Armor == null) continue;

                    Ship target = SelectTargetForShip(ship, squadron, enemySquadrons);
                    if (target == null) continue;

                    Squadron targetSquadron = shipToSquadron[target];

                    if (ship.Gun.TryFire(ship, target, out var projectile))
                    {
                        if (projectile.HasArrived)
                        {
                            allShots.Add(projectile);
                        }
                        else
                        {
                            globalProjectiles.Add(projectile);
                            logger.LogTorpedoLaunch(ship, squadron, ship.Gun, target, targetSquadron, projectile.TurnsRemaining);
                        }
                    }
                    else
                    {
                        logger.LogReloading(ship, squadron, ship.Gun.CurrentCooldown, ship.Gun.ReloadTurns);
                    }
                }
            }

            var arrivedTorpedoes = globalProjectiles.Where(p => p.HasArrived).ToList();
            if (arrivedTorpedoes.Count > 0)
            {
                Console.WriteLine("\nФАЗА ТОРПЕД:");
                allShots.AddRange(arrivedTorpedoes);
            }

            allShots = allShots.OrderBy(x => random.Next()).ToList();

            foreach (var shot in allShots)
            {
                ApplyDamage(shot);
            }

            globalProjectiles.RemoveAll(p => p.HasArrived);

            foreach (var squadron in squadrons)
            {
                foreach (var ship in squadron.Ships)
                {
                    if (ship.Gun != null)
                        ship.Gun.EndTurn();
                }
            }
        }

        private Ship SelectTargetForShip(Ship ship, Squadron allySquadron, List<Squadron> enemySquadrons)
        {
            var shuffledEnemies = enemySquadrons.OrderBy(x => random.Next()).ToList();

            if (allySquadron.Tactic != null)
            {
                foreach (var enemySquadron in shuffledEnemies)
                {
                    var target = allySquadron.Tactic.SelectTarget(ship, enemySquadron, allySquadron);
                    if (target != null) return target;
                }
            }

            var allEnemies = shuffledEnemies.SelectMany(s => s.GetAliveShips()).ToList();
            if (allEnemies.Count == 0) return null;
            return allEnemies[random.Next(allEnemies.Count)];
        }

        private void ApplyDamage(Projectile shot)
        {
            Ship target = shot.Target;
            Ship attacker = shot.Attacker;
            Gun gun = shot.Gun;
            Ammunition ammo = shot.Ammo;

            Squadron attackerSquadron = shipToSquadron[attacker];
            Squadron targetSquadron = shipToSquadron[target];

            if (!target.IsAlive)
            {
                logger.LogMiss(attacker, attackerSquadron, target, targetSquadron);
                return;
            }

            if (target.TryEvade())
            {
                logger.LogEvade(target, targetSquadron);
                return;
            }

            float damage = DamageCalculator.CalculateHit(gun, ammo, target.Armor);
            target.TakeDamage(damage);

            if (ammo.Type == AmmoType.Torped)
            {
                logger.LogTorpedoHit(attacker, attackerSquadron, target, targetSquadron, damage);
            }
            else
            {
                logger.LogShot(attacker, attackerSquadron, gun, target, targetSquadron, damage);
            }

            if (!target.IsAlive)
            {
                logger.LogDeath(target, targetSquadron);
                return;
            }

            if (target.TryRicochet())
            {
                HandleRicochet(attacker, attackerSquadron, target, targetSquadron, damage);
            }
        }

        private void HandleRicochet(Ship originalAttacker, Squadron attackerSquadron,
                                    Ship ricochetSource, Squadron sourceSquadron, float originalDamage)
        {
            var allShips = squadrons.SelectMany(s => s.GetAliveShips())
                                    .Where(s => s != ricochetSource)
                                    .ToList();

            if (allShips.Count == 0) return;

            Ship ricochetTarget = allShips[random.Next(allShips.Count)];
            Squadron targetSquadron = shipToSquadron[ricochetTarget];

            float ricochetDamage = originalDamage * 0.5f;

            if (ricochetTarget.TryEvade())
            {
                logger.LogEvade(ricochetTarget, targetSquadron);
                return;
            }

            ricochetTarget.TakeDamage(ricochetDamage);
            logger.LogRicochet(ricochetSource, sourceSquadron, ricochetTarget, targetSquadron, ricochetDamage);

            if (!ricochetTarget.IsAlive)
            {
                logger.LogDeath(ricochetTarget, targetSquadron);
            }
        }

        private void ProcessArrivedTorpedoes()
        {
            var arrived = globalProjectiles.Where(p => p.HasArrived).ToList();
            if (arrived.Count == 0) return;

            Console.WriteLine("\nФАЗА ТОРПЕД:");

            foreach (var torpedo in arrived)
            {
                ApplyDamage(torpedo);
            }

            globalProjectiles.RemoveAll(p => p.HasArrived);
        }

        private bool IsBattleOver()
        {
            int aliveSquadrons = squadrons.Count(s => s.HasAliveShips());
            return aliveSquadrons <= 1;
        }

        private void PrintSquadronInfo(Squadron squadron)
        {
            Console.ForegroundColor = logger.GetColor(squadron);
            Console.WriteLine($"\n{squadron.Name} (тактика: {squadron.Tactic?.Name ?? "Нет"})");
            Console.ResetColor();

            foreach (var ship in squadron.Ships)
            {
                string gunInfo = ship.Gun != null ? ship.Gun.Name : "Нет орудия";
                string armorInfo = ship.Armor != null ? ship.Armor.Name : "Нет брони";

                Console.ForegroundColor = logger.GetColor(squadron);
                Console.Write($"  {ship.Name} ({ship.Type})");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($" | HP: {ship.MaxHP} | {gunInfo} | {armorInfo}");
                Console.ResetColor();
            }
        }

        private void PrintBattleResults()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n╔{new string('═', 39)}╗");
            Console.WriteLine("║           ИТОГИ МОРСКОГО БОЯ          ║");
            Console.WriteLine($"╚{new string('═', 39)}╝");
            Console.ResetColor();

            Console.WriteLine($"\nБой длился {turnCount} ходов\n");

            foreach (var squadron in squadrons)
            {
                bool survived = squadron.HasAliveShips();

                Console.ForegroundColor = survived ? ConsoleColor.Green : ConsoleColor.Red;
                string result = survived ? "ПОБЕДА" : "ПОРАЖЕНИЕ";
                Console.Write($"{result} — ");

                Console.ForegroundColor = logger.GetColor(squadron);
                Console.WriteLine(squadron.Name);
                Console.ResetColor();

                foreach (var ship in squadron.Ships)
                {
                    if (ship.IsAlive)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"{ship.Name} — выжил ");
                        Console.WriteLine($"[HP: {ship.CurrentHP:F0}/{ship.MaxHP:F0}]");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{ship.Name} — уничтожен");
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}