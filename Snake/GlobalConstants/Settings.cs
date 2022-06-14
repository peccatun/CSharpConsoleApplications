namespace Snake.GlobalConstants
{
    public static class Settings
    {
        private const int fieldHeight = 40 ;
        private const int fieldStart = 3;
        private const int initialX = 1;
        public const int FieldWidht = 100;
        public const int ConsoleWidth = 110;
        public const int ConsoleHeight = 60;
        public const int ScoreStart = 0;


        public static int InitialX => initialX + fieldStart;

        public static int FieldStart => fieldStart;

        public static int FieldHeight => fieldHeight + fieldStart;

        public static class Map 
        {
            private const int offsetY = 3;
            private const int offsetX = 0;

            public static int StartX => 0 + offsetX;

            public static int EndX => 100 + offsetX;

            public static int StartY => 0 + offsetY;

            public static int EndY => 40 + offsetY;
        }

    }
}
