namespace Snake.Directions
{
    public class DirectionUp : Direction
    {
        public DirectionUp(int x, int y)
        {
            X = x;
            Y = y + 1;
        }
    }
}
