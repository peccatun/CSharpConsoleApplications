using System;
using System.Collections.Generic;
using System.Linq;

namespace PrisonersRiddle
{
    class Program
    {
        public struct GameData
        {
            public int randomWins;
            public int randomLoses;
            public int modelWins;
            public int modelLoses;
        }

        static void Main(string[] args)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Generator generator = new Generator(random);
            Shuffler shuffler = new Shuffler(random);
            GameData gameData = new GameData();

            gameData.randomWins = 0;
            gameData.randomLoses = 0;
            gameData.modelWins = 0;
            gameData.modelLoses = 0;

            while (true)
            {
                Play(generator, shuffler, ref gameData);
                PrintGameResults(gameData);

                Console.Write("Press any key to play again... 0 to exit: ");
                string input = Console.ReadLine();

                if (input.CompareTo("0") == 0)
                {
                    break;
                }

                Console.Clear();
                PrintGameResults(gameData);
            }

        }

        private static void Play(Generator generator, Shuffler shuffler, ref GameData gameData)
        {
            int boxesCount = GetBoxesCount();
            int prisonersCount = GetPrisonersCount();
            
            Box[] boxes = generator.GenerateBoxes(boxesCount);
            IEnumerable<Prisoner> prisoners = generator.GetPrisoners(prisonersCount);

            boxes = shuffler.ShuffleBoxes(boxes).ToArray();
            boxes = boxes.OrderBy(b => b.Number).ToArray();

            Room room = new Room(boxes);

            foreach (var prisoner in prisoners)
            {
                room.Prisoner = prisoner;
                room.Search();
            }

            string randomResults = room.GetResults();
            Console.WriteLine("Random opening results: ");
            Console.WriteLine(new string('*', 10) + "Model start" + new string('*', 10));
            Console.WriteLine("Adequate opening results: ");

            ResetPrisoners(prisoners);

            bool hasRandomFailed = room.Failed > 0;
            room.Reset();

            foreach (var prisoner in prisoners)
            {
                room.Prisoner = prisoner;
                room.SearchModel();
            }

            bool hasModelFailed = room.Failed > 0;

            string modelResults = room.GetResults();
            PrintCurrentResult(randomResults, modelResults, hasRandomFailed, hasModelFailed);
            ManageResults(ref gameData, hasModelFailed, hasRandomFailed);
        }

        private static int GetBoxesCount() 
        {
            Console.Write("Enter boxes count: ");
            int boxesCount = int.Parse(Console.ReadLine());
            return boxesCount;
        }

        private static int GetPrisonersCount() 
        {
            Console.Write("Enter prisoners count: ");
            int prisonersCount = int.Parse(Console.ReadLine());
            return prisonersCount;
        }

        private static void ResetPrisoners(IEnumerable<Prisoner> prisoners) 
        {
            for (int i = 0; i < prisoners.Count(); i++)
            {
                prisoners.ElementAt(i).Reset();
            }
        }

        private static void ManageResults(ref GameData gameData, bool hasModelFailed, bool hasRandomFailed)
        {
            if (hasModelFailed)
            {
                gameData.modelLoses++;
            }
            else
            {
                gameData.modelWins++;
            }

            if (hasRandomFailed)
            {
                gameData.randomLoses++;
            }
            else
            {
                gameData.randomWins++;
            }
        }

        private static void PrintCurrentResult(string randomResults, string modelResults, bool hasRandomFailed, bool hasModelFailed) 
        {
            Console.WriteLine("Random results: " + randomResults);
            Console.WriteLine("Model results: " + modelResults);
            Console.WriteLine($"has Random failed: {(hasRandomFailed ? "Yes" : "No")}, has Model failed: " +
                $" {(hasModelFailed ? "Yes" : "No")}");
        }

        private static void PrintGameResults(GameData gameData) 
        {
            Console.WriteLine($"Model results wins: {gameData.modelWins} loses {gameData.modelLoses}");
            Console.WriteLine($"Random results wins: {gameData.randomWins} loses {gameData.randomLoses}");
        }
    }


}
