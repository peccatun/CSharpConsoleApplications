namespace Snake.GameCore
{
    using System;
    using System.Threading;
    using Snake.SnakeCore;
    using Snake.Directions;
    using Snake.GlobalConstants;
    using Snake.Settings;

    public class Game : IGame
    {
        private readonly SnakeObject snake;
        private readonly MapSetting mapSetting;

        public Game(SnakeObject snake, MapSetting mapSetting)
        {
            this.snake = snake;
            this.mapSetting = mapSetting;
        }

        public void Start()
        {
            SetUpField();
            Engine();
        }

        private void Engine() 
        {
            new Thread(() =>
            {
                while (true)
                {
                    snake.Move();
                    Thread.Sleep(30);
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
            Console.SetWindowSize(Settings.ConsoleWidth, Settings.ConsoleHeight);
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

        }
    }
}
