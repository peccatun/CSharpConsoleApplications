using Snake.Directions;
using Snake.GlobalConstants;
using Snake.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.SnakeCore
{
    public class SnakeObject
    {

        private Orientation orientation;
        private Orientation prevOrientation;
        private bool hasTurned;
        private int currentX;
        private int currentY;
        private readonly SnakeSetting settings;

        private readonly Queue<ISnakePart> snakeParts;

        public SnakeObject(SnakeSetting settings)
        {
            hasTurned = true;
            currentX = settings.StartX;
            currentY = settings.StartY;
            orientation = Orientation.Right;
            snakeParts = new Queue<ISnakePart>();
            this.settings = settings;
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
                if (i > settings.EndX)
                {
                    currentX = settings.EndX;
                    currentY++;
                    orientation = Orientation.Down;
                }

                if (i >= settings.EndX - 2 + settings.EndY - 2)
                {
                    currentX = i -(settings.EndY - 2 + settings.EndX - 2);
                    currentY = settings.StartX - 2;
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
            if (currentX <= settings.StartX)
            {
                currentX = settings.EndX - 2;
            }

            if (currentX >= settings.EndX - 1)
            {
                currentX = settings.StartX + 1;
            }
        }

        private void AdjuctY()
        {
            if (currentY < settings.StartY)
            {
                currentY = settings.EndY - 2;
            }

            if (currentY > settings.EndY)
            {
                currentY = settings.StartY + 2;
            }
        }

        private void AdjustXy() 
        {
            AdjustX();
            AdjuctY();
        }
    }
}
