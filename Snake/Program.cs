namespace Snake
{
    using Snake.SnakeCore;
    using Snake.GameCore;
    using Snake.Settings;
    using Snake.GameObjects;

    public class Program
    {
        private const int START_X = 5;
        private const int END_X = 40;
        private const int START_Y = 5;
        private const int END_Y = 40;
        private const int OFFSET_X = 1;
        private const int OFFSET_Y = 3;

        static void Main(string[] args)
        {
            BaseSetting mapSetting = new MapSetting(START_X, END_X, START_Y, END_Y, OFFSET_X, OFFSET_Y);
            BaseSetting snakeSettings = new SnakeSetting(mapSetting);
            BaseSetting foodSetting = new FoodSetting(mapSetting);

            SnakeObject snake = new SnakeObject(snakeSettings);

            IFood food = new Food(foodSetting);
            IGame game = new Game(snake, mapSetting, food);
            game.Start();
        }
    }
}
