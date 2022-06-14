using Snake.Directions;
using Snake.GameObjects;
using Snake.Senses;
using Snake.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.SnakeCore
{
    public class SnakeObject : ISense
    {

        private Orientation orientation;
        private Orientation prevOrientation;
        private bool hasTurned;
        private int currentX;
        private int currentY;
        private readonly BaseSetting _snakeSetting;

        private readonly Queue<ISnakePart> snakeParts;

        public SnakeObject(BaseSetting snakeSetting)
        {
            hasTurned = true;
            currentX = snakeSetting.StartX;
            currentY = snakeSetting.StartY;
            orientation = Orientation.Right;
            snakeParts = new Queue<ISnakePart>();
            this._snakeSetting = snakeSetting;
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
                if (i > _snakeSetting.EndX)
                {
                    currentX = _snakeSetting.EndX;
                    currentY++;
                    orientation = Orientation.Down;
                }

                if (i >= _snakeSetting.EndX - 2 + _snakeSetting.EndY - 2)
                {
                    currentX = i -(_snakeSetting.EndY - 2 + _snakeSetting.EndX - 2);
                    currentY = _snakeSetting.StartX - 2;
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

            Direction direction = DirectionFactory(orientation, currentX, currentY);
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
            Direction direction = DirectionFactory(orientation, currentX, currentY);
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
            Direction direction = DirectionFactory(orientation, currentX, currentY);
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
            Direction direction = DirectionFactory(orientation, currentX, currentY);
            AddHead(direction);
            RemoveTail();

        }

        private Direction DirectionFactory(Orientation orientation, int x, int y) 
        {
            switch (orientation)
            {
                case Orientation.Up:
                    return new DirectionUp(x, y);
                case Orientation.Down:
                    return new DirectionDown(x, y);
                case Orientation.Left: 
                    return new DirectionLeft(x, y);
                case Orientation.Right:
                    return new DirectionRight(x, y);
                default:
                    return null;
            }
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
            if (currentX <= _snakeSetting.StartX)
            {
                currentX = _snakeSetting.EndX - 2;
            }

            if (currentX >= _snakeSetting.EndX - 1)
            {
                currentX = _snakeSetting.StartX + 1;
            }
        }

        private void AdjuctY()
        {
            if (currentY < _snakeSetting.StartY)
            {
                currentY = _snakeSetting.EndY - 2;
            }

            if (currentY > _snakeSetting.EndY)
            {
                currentY = _snakeSetting.StartY + 2;
            }
        }

        private void AdjustXy() 
        {
            AdjustX();
            AdjuctY();
        }

        public void Sense(ISenseble senseble, IFood food)
        {
            if (currentX == senseble.CurrentX && currentY == senseble.CurrentY)
            {
                Eat(food);
            }
        }

        private void Eat(IFood food) 
        {
            food.Destroy();
            Grow();
        }

        private void Grow() 
        {
            switch (orientation)
            {
                case Orientation.Up:
                    currentY--;
                    break;
                case Orientation.Down:
                    currentY++;
                    break;
                case Orientation.Left:
                    currentX--;
                    break;
                case Orientation.Right:
                    currentX++;
                    break;
                default:
                    break;
            }
            AdjustXy();
            Direction direction = DirectionFactory(orientation, currentX, currentY);
            AddHead(direction);
        }
    
    }
}
