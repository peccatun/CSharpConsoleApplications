using Snake.Directions;
using Snake.GlobalConstants;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    public class Program
    {


        static Task Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(Map.ConsoleWidth, Map.ConsoleHeight);
            DrawField();
            Snake snake = new Snake();

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

        static void DrawField() 
        {
            for (int i = 0; i < Map.FieldHeight; i++)
            {
                Console.SetCursorPosition(0,i);
                Console.Write('|');
                Console.SetCursorPosition(Map.FieldWidht, i);
                Console.Write('|');
            }

            for (int i = 0; i < Map.FieldWidht; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write('-');
                Console.SetCursorPosition(i, Map.FieldHeight);
                Console.Write('-');
            }
        }
    }
}
