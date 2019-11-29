using System;
namespace CMPT306_Checkers.Move
{
    public class Move
    {
        public int FromX { get; set; }
        public int FromY { get; set; }

        public int ToX { get; set; }
        public int ToY { get; set; }

        public bool Capture { get; set; }

        public Checker Checker { get; set; }

        public Checker Captured { get; set; }

        public Move()
        {

        }

        public Move(int toX, int toY, Checker checker, Checker captured = null)
        {
            ToX = toX;
            ToY = toY;
            Checker = checker;
            FromX = Checker.X;
            FromY = Checker.Y;

            if(captured != null)
            {
                Capture = true;
                Captured = captured;
            }
        }

        public Move(int fromX, int fromY, int toX, int toY, bool capture, Checker checker, Checker captured)
        {
            this.FromX = fromX;
            this.FromY = fromY;
            this.ToX = toX;
            this.ToY = toY;
        }
    }
}
