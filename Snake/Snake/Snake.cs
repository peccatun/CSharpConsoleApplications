using Snake.Directions;
using Snake.GlobalConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class Snake
    {
        private const int INITIAL_X = 1;
        private const int INITIAL_Y = 1;

        private Orientation orientation;
        private int currentX;
        private int currentY;

        private readonly Queue<ISnakePart> snakeParts;

        public Snake()
        {
            currentX = INITIAL_X;
            currentY = INITIAL_Y;
            orientation = Orientation.Right;
            snakeParts = new Queue<ISnakePart>();
            Initialize(15);
        }

        public Orientation Orientation 
        {
            set 
            {
                this.orientation = value;
            } 
        }

        public void Move()
        {
            switch (orientation)
            {
                case Orientation.Up:
                    MoveUp();
                    break;
                case Orientation.Down:
                    MoveDown();
                    break;
                case Orientation.Left:
                    MoveLeft();
                    break;
                case Orientation.Right:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        private void Initialize(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Direction dirrectionTail = new DirectionRight(currentX, currentY);
                currentX++;
                ISnakePart head = new SnakePart(dirrectionTail);
                head.Draw();
                snakeParts.Enqueue(head);
            }


        }

        private void MoveDown()
        {
            currentY++;
            if (currentY >= Map.FieldHeight - 1)
            {
                currentY = 1;
            }
            Direction direction = new DirectionDown(currentX, currentY);
            AddHead(direction);
            RemoveTail();
        }

        private void MoveLeft()
        {
            currentX--;
            if (currentX <= 1)
            {
                currentX = Map.FieldWidht - 1;
            }
            Direction direction = new DirectionLeft(currentX, currentY);
            AddHead(direction);
            RemoveTail();
        }

        private void MoveRight()
        {
            currentX++;
            if (currentX >= Map.FieldWidht )
            {
                currentX = 1;
            }
            Direction direction = new DirectionRight(currentX, currentY);
            AddHead(direction);
            RemoveTail();
        }

        private void MoveUp()
        {
            currentY--;
            if (currentY <= 1)
            {
                currentY = Map.FieldHeight - 1;
            }
            Direction direction = new DirectionUp(currentX, currentY);
            AddHead(direction);
            RemoveTail();
        }

        private void AddHead(Direction direction) 
        {
            ISnakePart head = new SnakePart(direction);
            head.Draw();
            snakeParts.Enqueue(head);
        }

        private void RemoveTail() 
        {
            var tail = snakeParts.Dequeue();
            tail.Dispose();
        }
    }
}
