using System;

namespace ConsoleApp1
{





    internal class Program
    {

        public enum AttackType
        {
            Damage,
            Self,
            Heal
        }

        static void Main(string[] args)
        {
            string? name;
            float maxPlayerHealth = 1000f;
            float maxEnemyHealth = 2000f;

            float currentPlayerHealth = maxPlayerHealth;
            float currentEnemyHealth = maxEnemyHealth;

            float playerDamage = 50f;
            float enemyDamage = 55f;

            float fireballDamage = 200f;
            float enemyHealthDamage = 100f;

            float healPlayer = 100f;

            string? select;
            int? input;

            bool playerShield;
            bool playerDamageWithWeapon;

            bool enemyDamageWithWeapon = false;

            
            
            int commandCount = 3;
            int enemySecondAbilityModifier = 3;

            Random rand = new Random();

            Dictionary<AttackType, List<float>> damagesToPlayer = new();
            Dictionary<AttackType, List<float>> damagesToEnemy = new();

            damagesToPlayer[AttackType.Damage] = new List<float>();
            damagesToPlayer[AttackType.Self] = new List<float>();
            damagesToPlayer[AttackType.Heal] = new List<float>();
            
            damagesToEnemy[AttackType.Damage] = new List<float>();
            damagesToEnemy[AttackType.Self] = new List<float>();
            damagesToEnemy[AttackType.Heal] = new List<float>();
            
            Console.Write("Введите ваше имя: ");
            name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine($"Привет, {name}! Вы начинаете игру. \n");

            
             void GetHealthStatus(float HealthPlayer, float maxHealth, string playerName )
            {
                if (HealthPlayer <= (0.3f * maxHealth))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (HealthPlayer <= (0.5f * maxHealth))
                { 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"Здоровье игрока {playerName}: {HealthPlayer}");
                Console.ResetColor();
            }

            void printDamage()
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            void playerAttack()
            {
                playerShield = false;
                playerDamageWithWeapon = false;

                
                
                switch (input)
                {
                    case 1:
                        playerDamageWithWeapon = true;
                        currentEnemyHealth -= playerDamage;
                        Console.WriteLine("Атака оружием");
                        damagesToEnemy[AttackType.Damage].Add(playerDamage);
                        break;
                    case 2:
                        playerShield = true;
                        Console.WriteLine("Вы использовали щит, атака противника не наносит урона.");
                        break;
                    case 3:
                        currentEnemyHealth -= fireballDamage;
                        Console.WriteLine("Атака магией");
                        damagesToEnemy[AttackType.Damage].Add(fireballDamage);
                        break;
                    case 4:
                        if (!enemyDamageWithWeapon)
                        {
                            currentPlayerHealth += healPlayer;
                            if (currentEnemyHealth > maxEnemyHealth)
                            {
                                currentEnemyHealth = maxEnemyHealth;
                            }
                            damagesToPlayer[AttackType.Heal].Add(healPlayer);
                        }
                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }

            void enemyAttack()
            {
                int enemyCommand = rand.Next(1, commandCount + 1);
                enemyDamageWithWeapon = false;
               
                switch (enemyCommand)
                {
                    case 1:
                        if (!playerShield)
                        {
                            enemyDamageWithWeapon = true;
                            currentPlayerHealth -= enemyDamage;
                            Console.WriteLine("обычная атака противника");
                            damagesToPlayer[AttackType.Damage].Add(enemyDamage);
                        }

                        break;
                    case 2:
                        if (!playerShield)
                        {
                            Console.WriteLine("другая Атака противника");
                            currentEnemyHealth -= enemyDamage;
                            currentPlayerHealth -= enemyDamage * enemySecondAbilityModifier;
                            Console.WriteLine("Противник наносит себе урон");
                            damagesToEnemy[AttackType.Self].Add(enemyDamage);
                            Console.WriteLine("противник Наносит урон игроку");
                            damagesToPlayer[AttackType.Damage].Add(enemyDamage * enemySecondAbilityModifier);
                        }

                        break;
                    case 3:
                        if (playerDamageWithWeapon)
                        {
                            currentPlayerHealth -= enemyHealthDamage;
                            Console.WriteLine("Обратный урон игроку");
                            damagesToPlayer[AttackType.Self].Add(enemyHealthDamage);
                        }
                        else
                        {
                            currentEnemyHealth += enemyHealthDamage;
                            Console.WriteLine("Противник восстанавливает здоровье");
                            damagesToEnemy[AttackType.Heal].Add(enemyHealthDamage);
                            if (currentEnemyHealth > maxEnemyHealth)
                            {
                                currentEnemyHealth = maxEnemyHealth;
                            }
                        }

                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }

            while (currentPlayerHealth > 0 && currentEnemyHealth > 0)
            {
                GetHealthStatus(currentPlayerHealth, maxPlayerHealth, name);
                GetHealthStatus(currentEnemyHealth,  maxEnemyHealth, "enemy");

                Console.WriteLine($"{name}! Выберите действие:\n" +
                                  $"1. Ударить оружием (урон {playerDamage})\n" +
                                  $"2. Использовать щит: атака противника не наносит урона\n" +
                                  $"3. Атаковать магией 'огненый шар': урон в размере {fireballDamage}\n" +
                                  $"4. Востановить здоровье! \n");
                
                select = Console.ReadLine();
                if (int.TryParse(select, out _ ))
                {
                    
                    input = Int32.Parse(select);
                }
                else
                {
                    Console.WriteLine("Неверный ввод.");
                    continue;
                }
                

                Console.WriteLine("Ход игрока");
                playerAttack();
                WriteDamageFromTo(damagesToEnemy, name, "Enemy");
                Console.WriteLine("================================================================");
                Console.WriteLine("Ход Противника");
                enemyAttack();
                WriteDamageFromTo(damagesToPlayer, "Enemy", name);
                EndTurn();
              

                void EndTurn()
                {
                    Console.WriteLine("\n Для продолжения нажите любую клавишу");
                    Console.ReadKey();
                    Console.Clear();
                }

                void DamageMessage(string damager, string defender, float damage)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        $"Игрок {damager} наносит угроку {defender} урон в размере {damage} едениц.");
                    Console.ResetColor();
                }

                void HealMessage(string healer, float healValue)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Игрок {healer} востановил себе здоровье в размере {healValue} едениц.");
                    Console.ResetColor();
                }

                void WriteDamageType(AttackType attackType, string damagerName, string defenderName,
                    List<float> damages)
                {
                    foreach (var damage in damages)
                    {
                        switch (attackType)
                        {
                            case AttackType.Damage:
                                DamageMessage(damagerName, defenderName, damage);
                                break;
                            case AttackType.Self:
                                DamageMessage(defenderName, defenderName, damage);
                                break;
                            case AttackType.Heal:
                                HealMessage(defenderName, damage);
                                break;
                        }
                    }
                    
                    damages.Clear();
                }

                void WriteDamageFromTo(Dictionary<AttackType, List<float>> damages, string damagerName, string defenderName)
                {
                    foreach (var damage in damages)
                    {
                        WriteDamageType(damage.Key, damagerName, defenderName, damage.Value);
                    }
                }
            }

            if (currentPlayerHealth > 0)
            {
                Console.WriteLine("Победил игрок {name}");
            }
            else
            {
                Console.WriteLine("Победил игрок Enemy");
            }
            
        }
    }

}