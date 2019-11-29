using System;
namespace CMPT306_Checkers.Move
{
    public struct Move
    {
        public int fromX { get; set; }
        public int fromY { get; set; }

        public int toX { get; set; }
        public int toY { get; set; }

        public int captureX { get; set; }
        public int captureY { get; set; }

        public Move(int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = oldY;
            this.toX = newX;
            this.toY = newY;
        }
    }
}
