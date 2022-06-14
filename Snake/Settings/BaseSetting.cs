namespace Snake.Settings
{
    public abstract class BaseSetting
    {
        /// <summary>
        /// |-->
        /// </summary>
        public abstract int StartX { get; }

        /// <summary>
        /// -->|
        /// </summary>
        public abstract int EndX { get; }

        /// <summary>
        /// _
        /// |
        /// V
        /// </summary>
        public abstract int StartY { get; }

        /// <summary>
        ///|
        ///v
        ///-
        /// </summary>
        public abstract int EndY { get; }
    }
}
