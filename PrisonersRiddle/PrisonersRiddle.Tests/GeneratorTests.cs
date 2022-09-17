using NUnit.Framework;
using PrisonersRiddle;
using System;
using System.Linq;

namespace PrisonersRiddle.Tests
{
   
    public class GeneratorTests
    {
        private const int BoxesCount = 100;

        private Random random;
        private Generator generator;

        [SetUp]
        public void StartUp() 
        {
            random = new Random(Guid.NewGuid().GetHashCode());
            generator = new Generator(random);
        }

        [Test]
        public void TestGenBoxesCountEqaul() 
        {
            Box[] boxes = generator.GenerateBoxes(BoxesCount);
            Assert.AreEqual(boxes.Length, BoxesCount);
        }

        [Test]
        public void TestGenBoxesCountNotEqaul() 
        {
            Box[] boxes = generator.GenerateBoxes(BoxesCount);
            Assert.AreNotEqual(boxes, BoxesCount + 1);
        }

        [Test]
        public void TestIsBoxNumbersUnique() 
        {
            Box[] boxes = generator.GenerateBoxes(BoxesCount);

            bool isUnique = true;

            for (int number = 1; number <= BoxesCount; number++)
            {
                int count = boxes.Count(b => b.Number.Equals(number));

                if (count > 1 || count == 0)
                {
                    isUnique = false;
                }
            }

            Assert.IsTrue(isUnique, "Box Number are not unique!");
        }

        [Test]
        public void TestIsBoxContentsUnique() 
        {
            Box[] boxes = generator.GenerateBoxes(BoxesCount);

            bool isUnique = true;

            for (int content = 1; content <= BoxesCount; content++)
            {
                int count = boxes.Count(b => b.Content.Equals(content));

                if (!count.Equals(1))
                {
                    isUnique = false;
                }
            }

            Assert.That(isUnique, "Box contents are not unique!");
        }
    }
}
