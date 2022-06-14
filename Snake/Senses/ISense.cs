using Snake.GameObjects;

namespace Snake.Senses
{
    public interface ISense
    {
        void Sense(ISenseble senseble, IFood food);
    }
}
