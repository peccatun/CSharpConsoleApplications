namespace Snake.GameCore
{
    using System;
    using System.Threading;
    using Snake.SnakeCore;
    using Snake.Directions;
    using Snake.Settings;
    using Snake.GameObjects;
    using Snake.Senses;

    public class Game : IGame
    {
        private readonly SnakeObject snake;
        private readonly BaseSetting mapSetting;
        private readonly IFood food;

        public Game(SnakeObject snake, BaseSetting mapSetting, IFood food)
        {
            this.snake = snake;
            this.mapSetting = mapSetting;
            this.food = food;
        }

        public void Start()
        {
            SetUpField();
            Engine();
        }

        private void Engine() 
        {
            food.Spawn();

            new Thread(() =>
            {
                while (true)
                {
                    snake.Move();
                    snake.Sense(food as ISenseble, food);
                    Thread.Sleep(60);
                }
            }).Start();

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey();


                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        snake.Orientation = Orientation.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        snake.Orientation = Orientation.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        snake.Orientation = Orientation.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        snake.Orientation = Orientation.Up;
                        break;
                    default:
                        break;
                };

            };
        }

        private void SetUpField() 
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(150, 50);
            DrawField();
        }

        private void DrawField()
        {
            for (int i = mapSetting.StartY; i < mapSetting.EndY; i++)
            {
                Console.SetCursorPosition(mapSetting.StartX, i);
                Console.Write('+');
                Console.SetCursorPosition(mapSetting.EndX, i);
                Console.Write('+');
            }

            for (int i = mapSetting.StartX; i < mapSetting.EndX; i++)
            {
                Console.SetCursorPosition(i, mapSetting.StartY);
                Console.Write('+');
                Console.SetCursorPosition(i, mapSetting.EndY);
                Console.Write('+');
            }
        }

        private void DrawScoreMap() 
        {
            throw new NotImplementedException();
        }
    }
}
