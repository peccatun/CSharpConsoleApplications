using NUnit.Framework;
using System;
using System.Linq;

namespace PrisonersRiddle.Tests
{
    public class ShufflerTests
    {
        private const int BoxesCount = 100;

        private Generator generator;
        private Shuffler shuffler;
        private Random random;
        private Box[] uniqueBoxes;
        private Box[] notUniqueBoxes;

        [SetUp]
        public void SetUp()
        {
            random = new Random(Guid.NewGuid().GetHashCode());
            generator = new Generator(random);
            shuffler = new Shuffler(random);

            uniqueBoxes = generator.GenerateBoxes(BoxesCount);
            uniqueBoxes = shuffler.ShuffleBoxes(uniqueBoxes).ToArray();

            notUniqueBoxes = generator.GenerateBoxes(BoxesCount);
            notUniqueBoxes = shuffler.ShuffleBoxes(notUniqueBoxes).ToArray();
            notUniqueBoxes[0].Number = notUniqueBoxes[1].Number;
            notUniqueBoxes[0].Content = notUniqueBoxes[1].Content;
        }

        [Test]
        public void TestIsShuffleBoxNumberUnique()
        {
            bool isUnique = IsUniqueNumbers(uniqueBoxes);
            Assert.IsTrue(isUnique, "Box numbers are not unique or are missing after shuffle!");
        }

        [Test]
        public void TestIsShuffleBoxContentUnique()
        {
            bool isUnique = IsUniqueContent(uniqueBoxes);
            Assert.IsTrue(isUnique, "Box content is not unique or is missing!");
        }

        [Test]
        public void TestIsShuffleNumbersNotUnique()
        {
            bool isUnique = IsUniqueNumbers(notUniqueBoxes);
            Assert.IsTrue(!isUnique, "Box numbers are unique when they should not be!");
        }

        [Test]
        public void TestIsShuffleContentNotUnique() 
        {
            bool isUnique = IsUniqueContent(notUniqueBoxes);
            Assert.IsTrue(!isUnique, "Box content is unique when it should not be!");
        }

        private bool IsUniqueNumbers(Box[] boxes) 
        {
            for (int number = 1; number <= BoxesCount; number++)
            {
                int count = boxes.Count(b => b.Number.Equals(number));

                if (count != 1)
                {
                    return  false;
                }
            }

            return true;
        }

        private bool IsUniqueContent(Box[] boxes) 
        {
            for (int content = 1; content <= BoxesCount; content++)
            {
                int count = boxes.Count(b => b.Content.Equals(content));

                if (count != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
