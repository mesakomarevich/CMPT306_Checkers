using System;
using System.Collections.Generic;
using System.Linq;

namespace CMPT306_Checkers
{
    /*
     * Checkers may only move diagonally and onto black tiles.
     * Chckers Movement:
     *      May only move diagonally and forward onto black tiles
     *      Normal move may only move one tile
     *      
     *      Capturing move jumps over an opposing checker
     *      Can only jump over a single checker at a time
     *      Multiple jumps can be made per turn
     *      If a capture can be made, it must be made
     *
     *      When a piece reaches the end row, it become a king and can move
     *      diagonally backward and forward
     */
    public class Game
    {
        public Checker[,] Board { get; set; }

        public List<Checker> Blacks { get; set; }

        public List<Checker> Reds { get; set; }

        public Game()
        {
            Board = new Checker[8, 8];

            //for (int i = 0; i < 8; i++)
            //{
            //    for (int n = 0; n < 8; n++)
            //    {
            //        Board[i, n] = new Tile((CMPT306_Checkers.Color)(((i % 2) + n) % 2));
            //    }
            //}

            Blacks = new List<Checker>
            {
                new Checker(0, Color.Black, 0, 0, false),
                new Checker(1, Color.Black, 2, 0, false),
                new Checker(2, Color.Black, 4, 0, false),
                new Checker(3, Color.Black, 6, 0, false),

                new Checker(4, Color.Black, 1, 1, false),
                new Checker(5, Color.Black, 3, 1, false),
                new Checker(6, Color.Black, 5, 1, false),
                new Checker(7, Color.Black, 7, 1, false),

                new Checker(8, Color.Black, 0, 2, false),
                new Checker(9, Color.Black, 2, 2, false),
                new Checker(10, Color.Black, 4, 2, false),
                new Checker(11, Color.Black, 6, 2, false),
            };

            Reds = new List<Checker>
            {
                new Checker(0, Color.Red, 1, 5, false),
                new Checker(1, Color.Red, 3, 5, false),
                new Checker(2, Color.Red, 5, 5, false),
                new Checker(3, Color.Red, 7, 5, false),

                new Checker(4, Color.Red, 0, 6, false),
                new Checker(5, Color.Red, 2, 6, false),
                new Checker(6, Color.Red, 4, 6, false),
                new Checker(7, Color.Red, 6, 6, false),

                new Checker(8, Color.Red, 1, 7, false),
                new Checker(9, Color.Red, 3, 7, false),
                new Checker(10, Color.Red, 5, 7, false),
                new Checker(11, Color.Red, 7, 7, false),
            };

            for (int i = 0; i < Blacks.Count; i++)
            {
                var black = Blacks[i];
                var red = Reds[i];
                Board[black.Y, black.X] = black;
                Board[red.Y, red.X] = red;
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
            for (int i = 0; i < 8; i++)
            {
                string row = $"{i}";
                for (int n = 0; n < 8; n++)
                {
                    //Color color = Board[i, n].Checker?.Color ?? Board[i, n].Color;

                    if (Board[i, n] != null)
                    {
                        Color color = Board[i, n].Color;

                        if (color == Color.Black)
                        {
                            row += " B";
                        }
                        else
                        {
                            row += " R";
                        }
                    }
                    else
                    {
                        row += "  ";
                    }
                }
                Console.WriteLine(row);
            }
        }

        public int BlackHeuristic(ref int[] move)
        {
            int capture = 100;

            int distanceMult = 20;

            int score = 0;

            int y = move[2];
            int x = move[3];

            //if no checker was captured
            if(move.Length == 4)
            {
                foreach (Checker red in Reds)
                {
                    score += distanceMult - (Math.Abs(x - red.X) + Math.Abs(y - red.Y));
                }
            }
            //if no checker was captured, we can ignore the check
            else
            {
                foreach(Checker red in Reds)
                {
                    if(red.Y != move[4] && red.X != move[5])
                    {
                        score += distanceMult - (Math.Abs(x - red.X) + Math.Abs(y - red.Y));
                    }
                }
                score += capture;
            }
            return score;
        }

        public int RedHeuristic(ref int[] move)
        {
            int capture = -100;

            int distanceMult = -20;

            int score = 0;

            int y = move[2];
            int x = move[3];

            //if no checker was captured
            if (move.Length == 4)
            {
                foreach (Checker black in Blacks)
                {
                    score += distanceMult + (Math.Abs(x - black.X) + Math.Abs(y - black.Y));
                }
            }
            //if no checker was captured, we can ignore the check
            else
            {
                foreach (Checker black in Blacks)
                {
                    if (black.Y != move[4] && black.X != move[5])
                    {
                        score += distanceMult - (Math.Abs(x - black.X) + Math.Abs(y - black.Y));
                    }
                }
                score += capture;
            }
            return score;
        }

        public void CheckLeft(Checker checker, List<int[]> moves)
        {
            if (checker.Y < 7 && checker.X > 0)
            {
                Checker leftTile = Board[checker.Y + 1, checker.X - 1];

                //leftTile has a checker of the opposite colour on it
                if (leftTile != null && leftTile.Color != checker.Color
                    //make sure we can potentially jump over it
                    && checker.Y + 1 < 7 && checker.X - 1 > 0)
                {
                    if (Board[checker.Y + 2, checker.X - 2] == null)
                    {
                        moves.Add(new int[] {
                            checker.Y, checker.X,           //from
                            checker.Y + 2, checker.X - 2,   //to
                            checker.Y + 1, checker.X - 1 });//capture
                    }
                }
                //check if we can move left
                else if (leftTile == null)
                {
                    moves.Add(new int[]
                    {
                        checker.Y, checker.X,           //from
                        checker.Y + 1, checker.X - 1    //to
                    });
                }
            }
        }

        public void CheckRight(Checker checker, List<int[]> moves)
        {
            if (checker.Y < 7 && checker.X < 7)
            {
                Checker leftTile = Board[checker.Y + 1, checker.X + 1];

                //leftTile has a checker of the opposite colour on it
                if (leftTile != null && leftTile.Color != checker.Color
                    //make sure we can potentially jump over it
                    && checker.Y + 1 < 7 && checker.X + 1 < 7)
                {
                    if (Board[checker.Y + 2, checker.X + 2] == null)
                    {
                        moves.Add(new int[] {
                            checker.Y, checker.X,           //from
                            checker.Y + 2, checker.X + 2,   //to
                            checker.Y + 1, checker.X + 1 });//capture
                    }
                }
                //check if we can move left
                else if (leftTile == null)
                {
                    moves.Add(new int[]
                    {
                        checker.Y, checker.X,           //from
                        checker.Y + 1, checker.X + 1    //to
                    });
                }
            }
        }

        public int BlackMove(int depth)
        {
            int best = 0;
            int curr = 0;
            int[] bestMove;
            List<int[]> moves = new List<int[]>();

            foreach(var black in Blacks)
            {
                CheckLeft(black, moves);
                CheckRight(black, moves);
            }

            foreach(var move in moves)
            {
                curr = BlackHeuristic(ref move);
                if(curr > best)
                {
                    bestMove = move;
                }
            }

        }

        public int RedMove(int depth)
        {

        }
    }
}
