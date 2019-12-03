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

    public class Move
    {
        public int FromX { get; set; }
        public int FromY { get; set; }

        public int ToX { get; set; }
        public int ToY { get; set; }

        public int Score { get; set; }

        public bool Capture { get; set; }

        public int CaptureX { get; set; }
        public int CaptureY { get; set; }

        public bool King { get; set; }

        public Move(int fromY, int fromX, int toY, int toX, 
            bool king = false, bool capture = false, 
            int captureY = -1, int captureX = -1, int score = 0)
        {
            FromY = fromY;
            FromX = fromX;

            ToY = toY;
            ToX = toX;

            King = king;

            Score = score;

            Capture = capture;

            CaptureY = captureY;
            CaptureX = captureX;
        }

        public Move(Move move)
        {
            FromY = move.FromY;
            FromX = move.FromX;
            
            ToY = move.ToY;
            ToX = move.ToX;

            King = move.King;

            Score = move.Score;

            Capture = move.Capture;

            CaptureY = move.CaptureY;
            CaptureX = move.CaptureX;
        }
    }
}
