namespace Snake.Settings
{
    public class SnakeSetting : BaseSetting
    {
        private readonly BaseSetting _setting;

        public SnakeSetting(BaseSetting setting)
        {
            _setting = setting;
        }

        public override int StartX => _setting.StartX + 1;

        public override int EndX => _setting.EndX - 1;

        public override int StartY => _setting.StartY + 1;

        public override int EndY => _setting.EndY - 1;
    }
}
