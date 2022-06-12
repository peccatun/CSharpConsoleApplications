using Snake.Directions;
using Snake.GlobalConstants;
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

            if (direction.Y == 0 || direction.Y >= Map.FieldHeight)
            {
                Console.WriteLine('-');
                return;
            }

            Console.Write(' ');
        }

        public void Draw()
        {
            SetCursorPosition();
            Console.Write('*');
        }

        private void SetCursorPosition()
        {
            Console.SetCursorPosition(direction.X, direction.Y);
        }
    }
}
