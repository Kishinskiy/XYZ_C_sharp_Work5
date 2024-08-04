using System;

namespace ConsoleApp1
{





    internal class Program
    {

        static void Main(string[] args)
        {
            Input input = new Input();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            Unit PlayerUnit = new Unit(50, 1000);
            Unit EnemyUnit = new Unit(55, 2000);
            
            
            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            Player player = new Player(name, PlayerUnit, EnemyUnit, input, consoleWriter);
            Enemy enemy = new Enemy(PlayerUnit, player.EnemyUnit, input);

            Console.Clear();
            Console.WriteLine($"Привет, {name}! Вы начинаете игру. \n");

            while (PlayerUnit.isAlive && EnemyUnit.isAlive)
            {
                player.Turn();
                enemy.Turn();

                consoleWriter.WriteDamageFromTo(EnemyUnit.DamageHistory, name, "Enemy");
                consoleWriter.WriteDamageFromTo(PlayerUnit.DamageHistory, "Enemy", name);

                EndTurn();


            }

            void EndTurn()
            {
                Console.WriteLine($"\nДля продолжения нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }

}