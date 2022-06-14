using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Settings
{
    public class MapSetting
    {
        private readonly int startX;
        private readonly int endX;
        private readonly int startY;
        private readonly int endY;
        private readonly int offsetX;
        private readonly int offsetY;

        public MapSetting(int startX, int endX, int startY, int endY, int offsetX, int offsetY)
        {
            this.startX = startX;
            this.endX = endX;
            this.startY = startY;
            this.endY = endY;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public int StartX => startX + offsetX;

        public int EndX => endX + offsetX;

        public int StartY => startY + offsetY;

        public int EndY => endY + offsetY;
    }
}
