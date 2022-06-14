namespace Snake
{
    using Snake.SnakeCore;
    using Snake.GameCore;
    using Snake.Settings;

    public class Program
    {
        private const int START_X = 0;
        private const int END_X = 100;
        private const int START_Y = 0;
        private const int END_Y = 40;
        private const int OFFSET_X = 1;
        private const int OFFSET_Y = 3;

        static void Main(string[] args)
        {
            MapSetting mapSetting = new MapSetting(START_X, END_X, START_Y, END_Y, OFFSET_X, OFFSET_Y);
            SnakeSetting snakeSettings = new SnakeSetting(mapSetting);
            SnakeObject snake = new SnakeObject(snakeSettings);
            IGame game = new Game(snake, mapSetting);
            game.Start();
        }
    }
}
