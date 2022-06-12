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
        private Orientation prevOrientation;
        private bool hasTurned;
        private int currentX;
        private int currentY;

        private readonly Queue<ISnakePart> snakeParts;

        public Snake()
        {
            hasTurned = true;
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
                prevOrientation = orientation;
                hasTurned = false;
                orientation = value;
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
                if (i > Map.FieldWidht - 2)
                {
                    currentX = Map.FieldWidht - 2;
                    currentY++;
                    orientation = Orientation.Down;
                }

                if (i >= Map.FieldWidht - 2 + Map.FieldHeight - 2)
                {
                    currentX = i -(Map.FieldHeight - 2 + Map.FieldWidht - 2);
                    currentY = Map.FieldHeight - 2;
                    orientation = Orientation.Left;
                }
                ISnakePart head = new SnakePart(dirrectionTail);
                head.Draw();
                snakeParts.Enqueue(head);
            }


        }

        private void MoveDown()
        {
            currentY++;
            if (prevOrientation == Orientation.Left && !hasTurned)
            {
                hasTurned = true;
                currentX--;
            }

            if (prevOrientation == Orientation.Right && !hasTurned)
            {
                hasTurned = true;
                currentX++;
            }

            AdjustXy();

            Direction direction = new DirectionDown(currentX, currentY);
            AddHead(direction);
            RemoveTail();
        }

        private void MoveUp()
        {
            currentY--;
            if (prevOrientation == Orientation.Left && !hasTurned)
            {
                hasTurned = true;
                currentX--;
            }

            if (prevOrientation == Orientation.Right && !hasTurned)
            {
                hasTurned = true;
                currentX++;
            }

            AdjustXy();
            Direction direction = new DirectionUp(currentX, currentY);
            AddHead(direction);
            RemoveTail();
        }

        private void MoveLeft()
        {
            currentX--;

            if (prevOrientation == Orientation.Up && !hasTurned)
            {
                hasTurned = true;
                currentY++;
            }

            if (prevOrientation == Orientation.Down && !hasTurned)
            {
                hasTurned = true;
                currentY--;
            }

            AdjustXy();
            Direction direction = new DirectionLeft(currentX, currentY);
            AddHead(direction);
            RemoveTail();

        }

        private void MoveRight()
        {
            currentX++;

            if (prevOrientation == Orientation.Up && !hasTurned)
            {
                hasTurned = true;
                currentY++;
            }

            if (prevOrientation == Orientation.Down && !hasTurned)
            {
                hasTurned = true;
                currentY--;
            }

            AdjustXy();
            Direction direction = new DirectionRight(currentX, currentY);
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

        private void AdjustX() 
        {
            if (currentX <= 1)
            {
                currentX = Map.FieldWidht - 2;
            }

            if (currentX >= Map.FieldWidht - 1)
            {
                currentX = 1;
            }
        }

        private void AdjuctY()
        {
            if (currentY < 0)
            {
                currentY = Map.FieldHeight - 2;
            }

            if (currentY > Map.FieldHeight)
            {
                currentY = 2;
            }
        }

        private void AdjustXy() 
        {
            AdjustX();
            AdjuctY();
        }
    }
}
