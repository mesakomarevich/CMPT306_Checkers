using System;
using System.Collections.Generic;

namespace CMPT306_Checkers
{
    public class Game
    {
        public Tile[,] Board { get; set; }

        public List<Checker> Blacks { get; set; }

        public List<Checker> Reds { get; set; }

        public Game()
        {
            Board = new Tile[8, 8];

            Blacks = new List<Checker>();
            for (int i = 0; i < 3; i++)
            {
                int yPos = i;

                for (int n = 0; n < 4; n++)
                {
                    int xPos = (i % 2) + (n * 2);
                    Blacks.Add(new Checker(((i * 4) + n),
                        Color.Black, xPos, yPos));
                }
            }
        }
    }
}
