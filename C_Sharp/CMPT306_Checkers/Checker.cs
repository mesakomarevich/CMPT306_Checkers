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

        public List<MoveBound> MoveBounds { get; set; }

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

        public Checker(Checker checker)
        {
            Id = checker.Id;
            Color = checker.Color;
            X = checker.X;
            Y = checker.Y;
            King = checker.King;
            MoveBounds = checker.MoveBounds;
        }

        public void SetMoveBounds()
        {
            MoveBounds = new List<MoveBound>();
            if (Color == Color.Black || King)
            {
                MoveBounds.Add(MoveBound.UpLeft());
                MoveBounds.Add(MoveBound.UpRight());
            }
            else if(Color == Color.Red || King)
            {
                MoveBounds.Add(MoveBound.DownLeft());
                MoveBounds.Add(MoveBound.DownRight());
            }
        }

        public void MakeKing()
        {
            King = true;
            SetMoveBounds();
        }

        /// <summary>
        /// Gets the legal moves for this checker
        /// </summary>
        /// <param name="board">Current game board</param>
        /// <param name="moves">Current list of moves</param>
        /// <param name="captureFound">Indicated whether a capture has been found yet</param>
        //public void GetMovement(Checker[,] board, List<Move> moves, ref bool captureFound)
        //{
        //    //Capturing bounds are always added to the front of these lists so that
        //    //they are evaluated first.
        //    List<int> xs = new List<int>();
        //    List<int> ys = new List<int>();
        //    int direction = (int)Color;

        //    //Kings can move in all directions
        //    if ((Color == Color.Black || King) && (Y < 7))
        //    {
        //        ys.Add(1);
        //    }
        //    else if ((Color == Color.Red || King) && (Y > 0))
        //    {
        //        ys.Add(-1);
        //    }

        //    if (X > 0)
        //    {
        //        xs.Add(-1);
        //    }
        //    if (X < 7)
        //    {
        //        xs.Add(1);
        //    }

        //    //if (X > 0 && X < 7)
        //    //{
        //    //    xs.Add(X + 1);
        //    //    xs.Add(X - 1);
        //    //}
        //    //else if (X == 0)
        //    //{
        //    //    xs.Add(X + 1);
        //    //}
        //    //else if (X == 7)
        //    //{
        //    //    xs.Add(X - 1);
        //    //}

        //    //Construct the list of bounds
        //    foreach (var y in ys)
        //    {
        //        foreach (var x in xs)
        //        {
        //            //Don't bother adding this to the list if a capture move exists
        //            if (board[Y + y, X + x] == null && !captureFound)
        //            {
        //                moves.Add(new Move(Y + y, X + x, this, null));
        //            }
        //            else if (board[Y + y, X + x] != null && board[Y + y, X + x].Color != Color
        //                && ((y == 1 && Y < 6) || (y == -1 && Y > 1))
        //                && ((x == 1 && X < 6) || (x == -1 && X > 1))
        //                && board[Y + (y * 2), X + (x * 2)] == null)
        //            {
        //                //if the capture has just been found, erase the non-capturing moves
        //                if (!captureFound)
        //                {
        //                    moves = new List<Move> { new Move(Y + (y * 2), X + (x * 2), this, board[Y + y, X + x]) };
        //                    captureFound = true;
        //                }
        //                else
        //                {
        //                    moves.Add(new Move(Y + (y * 2), X + (x * 2), this, board[Y + y, X + x]));
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Gets the movement bounds for this checker.
        /// </summary>
        /// <returns></returns>
        //public List<Move> GetMovement(Checker[,] board)
        //{
        //    List<Move> moves = new List<Move>();

        //    //Capturing bounds are always added to the front of these lists so that
        //    //they are evaluated first.
        //    List<int> xs = new List<int>();
        //    List<int> ys = new List<int>();
        //    int direction = (int)Color;

        //    //Indicates that we have captured a checker
        //    bool captureFound = false;

        //    //Kings can move in all directions
        //    if ((Color == Color.Black || King) && (Y < 7))
        //    {
        //        ys.Add(Y + 1);
        //    }
        //    else if ((Color == Color.Red || King) && (Y > 0))
        //    {

        //        ys.Add(Y - 1);
        //    }

        //    if (X >= 0)
        //    {
        //        xs.Add(X + 1);
        //    }
        //    if (X <= 7)
        //    {
        //        xs.Add(X - 1);
        //    }

        //    //Construct the list of bounds
        //    foreach (var y in ys)
        //    {
        //        foreach (var x in xs)
        //        {
        //            //Don't bother adding this to the list if a capture move exists
        //            if (board[y, x] == null && !captureFound)
        //            {
        //                moves.Add(new Move(y, x, this, board[y, x]));
        //            }
        //            else if (board[y, x] != null && board[y, x].Color != Color
        //                && y < 6 && y > 1
        //                && x < 6 && x > 1
        //                && board[y * 2, x * 2] == null)
        //            {
        //                //if the capture has just been found, erase the non-capturing moves
        //                if (!captureFound)
        //                {
        //                    moves = new List<Move> { new Move(y, x, this, board[y, x]) };
        //                    captureFound = true;
        //                }
        //                else
        //                {
        //                    moves.Add(new Move(y, x, this, board[y, x]));
        //                }
        //            }
        //        }
        //    }

        //    return moves;
        //}
        //public List<int[]> GetMovement()
        //{
        //    List<int[]> bounds = new List<int[]>();

        //    //Capturing bounds are always added to the front of these lists so that
        //    //they are evaluated first.
        //    List<int> xs = new List<int>();
        //    List<int> ys = new List<int>();
        //    int direction = (int)Color;

        //    //Kings can move in all directions
        //    if (Color == Color.Black || King)
        //    {
        //        switch (Y)
        //        {
        //            case 6:
        //                ys.Add(Y + 1);
        //                break;

        //            case 7:
        //                break;

        //            default:
        //                ys.Add(Y + 2);
        //                ys.Add(Y + 1);
        //                break;

        //        }
        //    }
        //    else if (Color == Color.Red || King)
        //    {
        //        switch (Y)
        //        {
        //            case 1:
        //                ys.Add(Y - 1);
        //                break;

        //            case 0:
        //                break;

        //            default:
        //                ys.Add(Y - 2);
        //                ys.Add(Y - 1);
        //                break;
        //        }
        //    }

        //    switch (X)
        //    {
        //        //can only move right
        //        case 0:
        //            xs.Add(X + 1);
        //            break;

        //        //can only move one to the left and two to the right
        //        case 1:
        //            xs.Add(X + 2);
        //            xs.Add(X - 1);
        //            xs.Add(X + 1);
        //            break;

        //        case 6:
        //            xs.Add(X - 2);
        //            xs.Add(X - 1);
        //            xs.Add(X + 1);
        //            break;

        //        case 7:
        //            xs.Add(X - 1);
        //            break;

        //        default:
        //            xs.Add(X - 2);
        //            xs.Add(X + 2);
        //            xs.Add(X - 1);
        //            xs.Add(X + 1);
        //            break;
        //    }

        //    //Construct the list of bounds
        //    foreach (var y in ys)
        //    {
        //        foreach (var x in xs)
        //        {
        //            bounds.Add(new int[]{ y, x});
        //        }
        //    }

        //    return bounds;
        //}
    }

    public enum Color
    {
        Black = 1,
        Red = -1
    }
}