using System;
namespace CMPT306_Checkers
{
    //struct for calculating the movement bounds of a checker
    public struct MoveBound
    {
        public int xDir { get; set; }
        public int yDir { get; set; }

        public Predicate<int> xBound { get; set; }
        public Predicate<int> yBound { get; set; }

        public MoveBound(int newYDir, int newXDir,
            Predicate<int> newYBound, Predicate<int> newXBound)
        {
            yDir = newYDir;
            xDir = newXDir;

            yBound = newYBound;
            xBound = newXBound;
        }

        public static MoveBound UpLeft()
        {
            return new MoveBound(1, -1, (y) => { return y < 7; }, (x) => { return x > 0; });
        }

        public static MoveBound UpRight()
        {
            return new MoveBound(1, 1, (y) => { return y < 7; }, (x) => { return x < 7; });
        }

        public static MoveBound DownLeft()
        {
            return new MoveBound(-1, -1, (y) => { return y > 0; }, (x) => { return x > 0; });
        }

        public static MoveBound DownRight()
        {
            return new MoveBound(-1, 1, (y) => { return y > 0; }, (x) => { return x < 7; });
        }
    }

    public struct Move
    {
        public int FromX { get; set; }
        public int FromY { get; set; }

        public int ToX { get; set; }
        public int ToY { get; set; }

        public int Score { get; set; }

        public bool Capture { get; set; }

        public int CaptureX { get; set; }
        public int CaptureY { get; set; }


        public Move(int fromX, int fromY, int toY, int toX)
        {
            FromX = fromX;
            FromY = fromY;

            ToX = toX;
            ToY = toY;

            Score = 0;

            Capture = false;

            CaptureX = -1;
            CaptureY = -1;
        }

        public Move(int fromX, int fromY, int toY, int toX, int score)
        {
            FromX = fromX;
            FromY = fromY;

            ToX = toX;
            ToY = toY;

            Score = score;

            Capture = false;

            CaptureX = -1;
            CaptureY = -1;
        }


        public Move(int fromX, int fromY, int toY, int toX, int score, int captureX, int captureY)
        {
            FromX = fromX;
            FromY = fromY;

            ToX = toX;
            ToY = toY;

            Score = score;

            Capture = false;

            CaptureX = captureX;
            CaptureY = captureY;
        }
    }
}
