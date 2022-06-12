using Snake.Directions;
using System;

namespace Snake
{
    public class SnakePart : ISnakePart
    {
        private readonly Direction direction;

        public SnakePart(Direction direction)
        {
            this.direction = direction;
        }

        public void Dispose()
        {
            SetCursorPosition();
            Console.Write(' ');
        }

        public void Draw()
        {
            SetCursorPosition();
            Console.Write('o');
        }

        private void SetCursorPosition()
        {
            Console.SetCursorPosition(direction.X, direction.Y);
        }
    }
}
