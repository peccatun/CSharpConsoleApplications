namespace Snake.Settings
{
    public class SnakeSetting
    {
        private readonly MapSetting setting;

        public SnakeSetting(MapSetting setting)
        {
            this.setting = setting;
        }

        public int StartX => setting.StartX + 1;

        public int EndX => setting.EndX - 1;

        public int StartY => setting.StartY + 1;

        public int EndY => setting.EndY - 1;
    }
}
