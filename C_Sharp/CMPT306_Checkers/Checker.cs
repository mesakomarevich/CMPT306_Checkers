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

        //public List<MoveBound> MoveBounds { get; set; }
        public MoveBound[] MoveBounds { get; set; }

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
            SetMoveBounds();
        }

        //Basically a copy constructor
        public Checker(Checker checker)
        {
            Id = checker.Id;
            Color = checker.Color;
            X = checker.X;
            Y = checker.Y;
            King = checker.King;
            MoveBounds = checker.MoveBounds;
        }

        /// <summary>
        /// Sets the movement bounds for the checker
        /// </summary>
        public void SetMoveBounds()
        {
            //MoveBounds = new List<MoveBound>();
            if (King)
            {
                MoveBounds = new MoveBound[] 
                {
                    MoveBound.UpLeft(),
                    MoveBound.UpRight(),
                    MoveBound.DownLeft(),
                    MoveBound.DownRight()
                };
            }
            if (Color == Color.Black || King)
            {
                MoveBounds = new MoveBound[]
                {
                    MoveBound.UpLeft(),
                    MoveBound.UpRight()
                };
            }
            if (Color == Color.Red || King)
            {
                MoveBounds = new MoveBound[]
                {
                    MoveBound.DownLeft(),
                    MoveBound.DownRight()
                };
            }
            //if (Color == Color.Black || King)
            //{
            //    MoveBounds.Add(MoveBound.UpLeft());
            //    MoveBounds.Add(MoveBound.UpRight());
            //}
            //if (Color == Color.Red || King)
            //{
            //    MoveBounds.Add(MoveBound.DownLeft());
            //    MoveBounds.Add(MoveBound.DownRight());
            //}
        }

        public void MakeKing()
        {
            King = true;
            SetMoveBounds();
        }

        /// <summary>
        /// Checks if this checker will be made a king if it reaches the specified position
        /// </summary>
        /// <param name="yPos">y position to check</param>
        /// <returns>
        /// true if the checker will be made a king, false otherwise or if the checker is already a king
        /// </returns>
        public bool Kingable(int yPos)
        {
            bool kingable = false;

            if (!King)
            {
                if (Color == Color.Black && yPos == 7)
                {
                    kingable = true;
                }
                else if (Color == Color.Red && yPos == 0)
                {
                    kingable = true;
                }
            }

            return kingable;
        }
    }

    public enum Color
    {
        Black = 1,
        Red = -1
    }
}