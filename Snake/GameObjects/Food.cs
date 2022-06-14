using Snake.Senses;
using Snake.Settings;
using System;

namespace Snake.GameObjects
{
    public class Food : IFood, ISenseble
    {
        private readonly BaseSetting _setting;
        private readonly Random random;

        private int currentX;
        private int currentY;

        public Food(BaseSetting setting)
        {
            random = new Random();
            _setting = setting;
            currentX = -1;
            currentY = -1;
        }

        public int CurrentX => currentX;

        public int CurrentY => currentY;

        public void Destroy()
        {
            Console.SetCursorPosition(currentX, currentY);
            Console.Write(' ');
            Spawn();
        }

        public void Spawn()
        {
            currentX = random.Next(_setting.StartX, _setting.EndX);
            currentY = random.Next(_setting.StartY, _setting.EndY);
            Console.SetCursorPosition(currentX, currentY);
            Console.Write('8');
        }
    }
}
