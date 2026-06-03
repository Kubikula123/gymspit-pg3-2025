using System;
using Lecture18;

namespace Lecture18
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Console.WriteLine("========================================");
            Console.WriteLine("       WELCOME TO THE BATTLE ARENA      ");
            Console.WriteLine("========================================");

            Console.Write("Enter your character's name: ");
            string playerName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                playerName = "Tav"; 
            }

            // 2. Class Selection Menu
            Console.WriteLine("\nChoose your starting class:");
            Console.WriteLine("1. Fighter (High HP, High Armor, Medium Attack)");
            Console.WriteLine("2. Ranger  (Medium HP, Medium Armor, High Attack)");
            Console.WriteLine("3. Wizard  (Low HP, Low Armor, Very High Attack)");
            Console.Write("Enter 1, 2, or 3: ");

            string classChoice = Console.ReadLine();


            int hp = 0;
            int atk = 0;
            int def = 0; 


            HeroClass selectedClass = HeroClass.Fighter; 

            switch (classChoice)
            {
                case "1": 
                    hp = 40; atk = 5; def = 16;
                    selectedClass = HeroClass.Fighter;
                    break;
                case "2": 
                    hp = 30; atk = 7; def = 14;
                    selectedClass = HeroClass.Ranger;
                    break;
                case "3": 
                    hp = 20; atk = 9; def = 11;
                    selectedClass = HeroClass.Wizard;
                    break;
            }


            Character player = new Player(playerName, hp, atk, def, Console.In, Console.Out);


            Character c3PO = new NPC("C-3PO", 25, 4, 13, random);
            Character r2D2 = new NPC("R2-D2", 20, 6, 15, random);

            Console.WriteLine("\n--- BATTLE 1: DROIDS VS DROIDS ---");
            Game game1 = new Game(c3PO, r2D2, new Die(random, 6));
            game1.Run(Console.Out);

            c3PO.Reset();

            Console.WriteLine("\n--- BATTLE 2: PLAYER VS SURVIVOR ---");
            Game game2 = new Game(player, c3PO, new Die(random, 6));
            game2.Run(Console.Out);

            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadLine();
        }
    }
}