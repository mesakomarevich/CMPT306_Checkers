using System;
namespace CMPT306_Checkers.Move
{
    public struct Move
    {
        public int oldX { get; set; }
        public int oldY { get; set; }

        public int newX { get; set; }
        public int newY { get; set; }

        public Move(int oldX, int oldY, int newX, int newY)
        {
            this.oldX = oldX;
            this.oldY = oldY;
            this.newX = newX;
            this.newY = newY;
        }
    }
}
