using System;
using System.Collections.Generic;
using System.Linq;

namespace PrisonersRiddle
{
    public sealed class Generator
    {
        private readonly Random random;

        public Generator(Random random)
        {
            this.random = random;
        }

        public Box[] GenerateBoxes(int count)
        {
            return Enumerable.Range(1, count).Select(b => new Box(b, b)).OrderBy(x => random.Next()).ToArray();
        }

        public IEnumerable<Prisoner> GetPrisoners(int count)
        {
            return Enumerable.Range(1, count).Select(p => new Prisoner(random) { Number = p }).OrderBy(p => random.Next()).ToArray();
        }

        public void ValidateBoxes(IEnumerable<Box> boxes) 
        {
            for (int i = 1; i < boxes.Count(); i++)
            {
                int counter = 0;
                foreach (var box in boxes)
                {
                    if (box.Content == i)
                    {
                        counter++;
                    }
                }

                Console.WriteLine($"{i}" + (counter > 1 || counter == 0));
            }
        }
    }
}
