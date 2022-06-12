namespace Snake.Directions
{
    public class DirectionDown : Direction
    {
        public DirectionDown(int x, int y)
        {
            X = x;
            Y = y - 1;
        }
    }
}
