using System;
using System.Collections.Generic;

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
        public Tile[,] Board { get; set; }

        public List<Checker> Blacks { get; set; }

        public List<Checker> Reds { get; set; }

        public Game()
        {
            Board = new Tile[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int n = 0; n < 8; n++)
                {
                    Board[i, n] = new Tile((CMPT306_Checkers.Color)(((i % 2) + n) % 2));
                }
            }

            Blacks = new List<Checker>
            {
                new Checker(0, Color.Black, 0, 0),
                new Checker(1, Color.Black, 2, 0),
                new Checker(2, Color.Black, 4, 0),
                new Checker(3, Color.Black, 6, 0),

                new Checker(4, Color.Black, 1, 1),
                new Checker(5, Color.Black, 3, 1),
                new Checker(6, Color.Black, 5, 1),
                new Checker(7, Color.Black, 7, 1),

                new Checker(8, Color.Black, 0, 2),
                new Checker(9, Color.Black, 2, 2),
                new Checker(10, Color.Black, 4, 2),
                new Checker(11, Color.Black, 6, 2),
            };

            Reds = new List<Checker>
            {
                new Checker(0, Color.Red, 1, 5),
                new Checker(1, Color.Red, 3, 5),
                new Checker(2, Color.Red, 5, 5),
                new Checker(3, Color.Red, 7, 5),

                new Checker(4, Color.Red, 0, 6),
                new Checker(5, Color.Red, 2, 6),
                new Checker(6, Color.Red, 4, 6),
                new Checker(7, Color.Red, 6, 6),

                new Checker(8, Color.Red, 1, 7),
                new Checker(9, Color.Red, 3, 7),
                new Checker(10, Color.Red, 5, 7),
                new Checker(11, Color.Red, 7, 7),
            };

            for (int i = 0; i < Blacks.Count; i++)
            {
                var black = Blacks[i];
                var red = Reds[i];
                Board[black.Y, black.X].Checker = black;
                Board[red.Y, red.X].Checker = red;
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

                    if (Board[i, n].Checker != null)
                    {
                        Color color = Board[i, n].Checker.Color;

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


    }
}
