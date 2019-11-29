using System;
using System.Collections.Generic;

namespace CMPT306_Checkers
{
    public class Checker
    {
        public int Id { get; set; }

        public Color Color { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool King { get; set; }

        public Checker()
        {
        }

        public Checker(int newId, Color newColor, int newX, int newY, bool newKing) : this()
        {
            Id = newId;
            Color = newColor;
            X = newX;
            Y = newY;
            King = newKing;
        }

        public Checker(Checker checker)
        {
            Id = checker.Id;
            Color = checker.Color;
            X = checker.X;
            Y = checker.Y;
            King = checker.King;
        }

        /// <summary>
        /// Gets the movement bounds for this checker.
        /// </summary>
        /// <returns></returns>
        public List<int[]> GetMovement()
        {
            List<int[]> bounds = new List<int[]>();

            //Capturing bounds are always added to the front of these lists so that
            //they are evaluated first.
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            int direction = (int)Color;

            //Kings can move in all directions
            if (Color == Color.Black || King)
            {
                switch (Y)
                {
                    case 6:
                        ys.Add(Y + 1);
                        break;

                    case 7:
                        break;

                    default:
                        ys.Add(Y + 2);
                        ys.Add(Y + 1);
                        break;

                }
            }
            else if (Color == Color.Red || King)
            {
                switch (Y)
                {
                    case 1:
                        ys.Add(Y - 1);
                        break;

                    case 0:
                        break;

                    default:
                        ys.Add(Y - 2);
                        ys.Add(Y - 1);
                        break;
                }
            }

            switch (X)
            {
                //can only move right
                case 0:
                    xs.Add(X + 1);
                    break;

                //can only move one to the left and two to the right
                case 1:
                    xs.Add(X + 2);
                    xs.Add(X - 1);
                    xs.Add(X + 1);
                    break;

                case 6:
                    xs.Add(X - 2);
                    xs.Add(X - 1);
                    xs.Add(X + 1);
                    break;

                case 7:
                    xs.Add(X - 1);
                    break;

                default:
                    xs.Add(X - 2);
                    xs.Add(X + 2);
                    xs.Add(X - 1);
                    xs.Add(X + 1);
                    break;
            }

            //Construct the list of bounds
            foreach (var y in ys)
            {
                foreach (var x in xs)
                {
                    bounds.Add(new int[]{ y, x});
                }
            }

            return bounds;
        }
    }

    public enum Color
    {
        Black = 1,
        Red = -1
    }
}