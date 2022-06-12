namespace Snake.Directions
{
    public class DirectionRight : Direction
    {
        public DirectionRight(int x, int y)
        {
            X = x + 1;
            Y = y;
        }
    }
}
