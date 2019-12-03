using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMPT306_Checkers
{
    public class Heuristic
    {
        public int Win { get; set; }
        public int Capture { get; set; }
        public int DistanceMult { get; set; }
        public int King { get; set; }
        public int Score { get; set; }
        public Color Color { get; set; }

        public Heuristic()
        {

        }

        public Heuristic(int win, int capture, int distanceMult, int king, int score, Color color)
        {
            Win = win;
            Capture = capture;
            DistanceMult = distanceMult;
            King = king;
            Score = score;
            Color = color;
        }

        public static Heuristic DefaultHeuristic(Color color)
        {
            return new Heuristic(win: 10000, capture: 200, distanceMult: 20, king: 20, score: 0, color);
        }

        public int Calculate(Checker[,] board, List<Checker> blacks, List<Checker> reds)
        {
            double score = 0;
            List<Checker> players;
            List<Checker> opponents;

            players = Color == Color.Black ? blacks : reds;
            opponents = Color == Color.Black ? reds : blacks;

            score += players.Aggregate(0.0d,
                (pscore, player) => pscore += opponents.Aggregate(0.0d,
                    (oscore, opponent) =>
                            oscore += DistanceMult - 
                            //Euclidean distance
                            Math.Sqrt(Math.Pow(player.X - opponent.X, 2) + Math.Pow(player.Y - opponent.Y, 2))));

            
            score += players.Sum(x => x.King ? King : 0);

            if (opponents.Count != 0)
            {
                score += ((12 - opponents.Count) * Capture);
            }
            else
            {
                score += Win;
            }

            //Color is 1 for black and -1 for red
            return (int)Color * (int)score;
        }
    }
}
