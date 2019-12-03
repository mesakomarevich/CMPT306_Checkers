using System;
using System.Collections.Generic;
using System.Linq;

namespace CMPT306_Checkers
{
    //public interface IGameState<T>
    //{
    //    public Checker[,] Board { get; set; }

    //    public List<Checker> Blacks { get; set; }

    //    public List<Checker> Reds { get; set; }

    //    public Color Turn { get; set; }

    //    public int Depth { get; set; }

    //    public T Move { get; set; }

    //    public IGameState<T> ApplyMove();

    //    public IGameState<T> ApplyMove(T move);
    //}


    public class GameState //: IGameState<int[]>
    {
        public Checker[,] Board { get; set; }

        public List<Checker> Blacks { get; set; }

        public List<Checker> Reds { get; set; }

        public Color Turn { get; set; }

        public int Depth { get; set; }

        public int[] Move { get; set; }


        public GameState()
        {
        }

        public GameState(Checker[,] board, List<Checker> blacks, List<Checker> reds, Color turn, int depth, int[] move)
        {
            Board = board;
            Blacks = blacks;
            Reds = reds;
            Turn = turn;
            Depth = depth;
            Move = move;
        }

        //public GameState(GameState oldState)
        //{
        //    Board = new Checker[8, 8];
        //    Blacks = oldState.Blacks.Select(x => new Checker(x)).ToList();
        //    Reds = oldState.Reds.Select(x => new Checker(x)).ToList();
        //}

        public GameState ApplyMove()
        {
            return ApplyMove(Move);
        }

        //public IGameState<int[]> ApplyMove(int[] move)
        public GameState ApplyMove(int[] move)
        {
            Checker toMove;
            Checker[,] newBoard = new Checker[8, 8];
            List<Checker> newBlacks = Blacks.Select(x => new Checker(x)).ToList();
            List<Checker> newReds = Reds.Select(x => new Checker(x)).ToList();

            newBlacks.ForEach(x => newBoard[x.Y, x.X] = x);
            newReds.ForEach(x => newBoard[x.Y, x.X] = x);

            if(move != null)
            {
                toMove = newBoard[move[0], move[1]];
                //update board
                newBoard[move[2], move[3]] = toMove;
                newBoard[move[0], move[1]] = null;

                //update toMove
                toMove.Y = move[2];
                toMove.X = move[3];

                if (move.Length >= 6)
                {
                    Checker toRemove = newBoard[move[4], move[5]];

                    _ = toRemove.Color == Color.Black
                        ? newBlacks.Remove(toRemove)
                        : newReds.Remove(toRemove);

                    newBoard[move[4], move[5]] = null;
                    toRemove = null;
                }

                //make the checker a king if possible
                if (toMove.Kingable(toMove.Y))
                {
                    toMove.MakeKing();
                }
            }
            
            return new GameState(newBoard, newBlacks, newReds,
                Turn == Color.Black ? Color.Red : Color.Black, depth: -1, move);
        }
    }

    public class GameStateClass //: IGameState<Move>
    {
        public Checker[,] Board { get; set; }

        public List<Checker> Blacks { get; set; }

        public List<Checker> Reds { get; set; }

        public Color Turn { get; set; }

        public int Depth { get; set; }

        public Move Move { get; set; }

        public GameStateClass()
        {
        }

        public GameStateClass(Checker[,] board, List<Checker> blacks, List<Checker> reds, Color turn, int depth, Move move)
        {
            Board = board;
            Blacks = blacks;
            Reds = reds;
            Turn = turn;
            Depth = depth;
            Move = move;
        }

        //Applies the current move and returns a new gamestate
        //public IGameState<Move> ApplyMove()
        public GameStateClass ApplyMove()
        {
            return ApplyMove(Move);
        }

        //public IGameState<Move> ApplyMove(Move move)
        public GameStateClass ApplyMove(Move move)
        {
            Checker toMove;
            Checker[,] newBoard = new Checker[8, 8];
            List<Checker> newBlacks = Blacks.Select(x => new Checker(x)).ToList();
            List<Checker> newReds = Reds.Select(x => new Checker(x)).ToList();

            newBoard = new Checker[8, 8];
            newBlacks = Blacks.Select(x => new Checker(x)).ToList();
            newReds = Reds.Select(x => new Checker(x)).ToList();

            newBlacks.ForEach(x => newBoard[x.Y, x.X] = x);
            newReds.ForEach(x => newBoard[x.Y, x.X] = x);

            if (move != null)
            {
                toMove = newBoard[move.FromY, move.FromX];
                //update board
                newBoard[move.ToY, move.ToX] = toMove;
                newBoard[move.FromY, move.FromX] = null;

                //update toMove
                toMove.Y = move.ToY;
                toMove.X = move.ToX;

                if (move.Capture)
                {
                    Checker toRemove = newBoard[move.CaptureY, move.CaptureX];

                    _ = toRemove.Color == Color.Black
                        ? newBlacks.Remove(toRemove)
                        : newReds.Remove(toRemove);

                    newBoard[move.CaptureY, move.CaptureX] = null;
                    toRemove = null;
                }

                if (move.King)
                {
                    toMove.MakeKing();
                }
            }

            return new GameStateClass(newBoard, newBlacks, newReds,
                Turn == Color.Black ? Color.Red : Color.Black, depth: -1, move);
        }
    }
}
