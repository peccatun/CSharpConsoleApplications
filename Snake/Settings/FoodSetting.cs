namespace Snake.Settings
{
    public class FoodSetting : BaseSetting
    {
        private readonly BaseSetting _mapSetting;

        public FoodSetting(BaseSetting mapSetting)
        {
            _mapSetting = mapSetting;
        }

        public override int StartX => _mapSetting.StartX + 2;

        public override int EndX => _mapSetting.EndX - 1;

        public override int StartY => _mapSetting.StartY + 2;

        public override int EndY => _mapSetting.EndY - 1;
    }
}
