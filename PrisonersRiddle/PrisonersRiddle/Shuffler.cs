using System;
using System.Collections.Generic;
using System.Linq;

namespace PrisonersRiddle
{
    public sealed class Shuffler
    {
        private readonly Random random;

        public Shuffler(Random random)
        {
            this.random = random;
        }

        public IEnumerable<Box> ShuffleBoxes(IEnumerable<Box> boxes)
        {
            for (int i = 0; i < 100; i++)
            {
                boxes = Shuffle(boxes);
            }

            return boxes;
        }

        private IEnumerable<Box> Shuffle(IEnumerable<Box> boxes) 
        {
            boxes = boxes.Select(b => b).OrderBy(b => random.Next()).ToList();
            int count = boxes.Count();
            bool hasEnded = false;
            for (int i = 0; i < count;)
            {
                Box current = boxes.ElementAt(i++);
                if (i == count)
                {
                    i /= 2;
                    hasEnded = true;
                }
                Box next = boxes.ElementAt(i++);

                int contentTemp = current.Content;
                current.Content = next.Content;
                next.Content = contentTemp;

                if (hasEnded)
                {
                    break;
                }
            }

            return boxes;
        }
    }
}
