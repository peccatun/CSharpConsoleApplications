namespace Snake.Directions
{
    public class DirectionLeft : Direction
    {
        public DirectionLeft(int x, int y)
        {
            X = x - 1;
            Y = y;
        }
    }
}
