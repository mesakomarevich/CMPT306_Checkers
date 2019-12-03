using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

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

    /*
     * Should be able to take in a function for both blacks and reds to use to make moves
     * Should be able to take in a function for calculating the heuristics of blacks and reds
     */
    public class Game
    {
        public Checker[,] Board { get; set; }

        public List<Checker> Blacks { get; set; }

        public List<Checker> Reds { get; set; }

        public int MaxNodes { get; set; }

        public Stopwatch Watch { get; set; }

        public Random random = new Random();

        public Heuristic BlackHeuristic { get; set; }

        public Heuristic RedHeuristic { get; set; }

        public Game()
        {
            Board = new Checker[8, 8];

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

            MaxNodes = 0;

            Watch = new Stopwatch();

            BlackHeuristic = Heuristic.DefaultHeuristic(Color.Black);
            RedHeuristic = Heuristic.DefaultHeuristic(Color.Red);
        }

        public void Reset()
        {
            Board = new Checker[8, 8];

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

            MaxNodes = 0;

            Watch = new Stopwatch();
        }

        public void PrintBoard(Color? turn = null, int? score = null)
        {
            Console.WriteLine("\n");
            Console.WriteLine("  0 1 2 3 4 5 6 7");
            for (int i = 0; i < 8; i++)
            {
                string row = $"{i}";
                for (int n = 0; n < 8; n++)
                {
                    //Color color = Board[i, n].Checker?.Color ?? Board[i, n].Color;

                    if (Board[i, n] != null)
                    {
                        Checker checker = Board[i, n];

                        if (checker.Color == Color.Black)
                        {
                            row += checker.King ? " B" : " b";
                        }
                        else
                        {
                            row += checker.King ? " R" : " r";
                        }
                    }
                    else
                    {
                        row += "  ";
                    }
                }
                Console.WriteLine(row);
            }

            Console.WriteLine($"Blacks: {Blacks.Count}");
            Console.WriteLine($"Reds: {Reds.Count}");

            if (turn != null)
            {
                Console.WriteLine("Turn: {0}", turn == Color.Black ? "Black" : "Red");
            }

            if (score != null)
            {
                Console.WriteLine($"Score: {score}");
            }
        }

        public void CheckBoard(Checker checker, Checker[,] board,
            List<int[]> moves)
        {
            foreach (var bound in checker.MoveBounds)
            {
                //make sure the checker is in bounds
                if (bound.yBound(checker.Y) && bound.xBound(checker.X))
                {
                    Checker tile = board[checker.Y + bound.yDir, checker.X + bound.xDir];

                    //leftTile has a checker of the opposite colour on it
                    if (tile != null && tile.Color != checker.Color
                        //make sure we can potentially jump over it
                        && bound.yBound(checker.Y + bound.yDir)
                        && bound.xBound(checker.X + bound.xDir))
                    {
                        if (board[checker.Y + (2 * bound.yDir),
                            checker.X + (2 * bound.xDir)] == null)
                        {
                            moves.Add(new int[] {
                            checker.Y, checker.X,                                   //from
                            checker.Y + (2*bound.yDir), checker.X + (2*bound.xDir), //to
                            checker.Y + bound.yDir, checker.X + bound.xDir });      //capture
                        }
                    }
                    //check if we can move left
                    else if (tile == null)
                    {
                        moves.Add(new int[]
                        {
                            checker.Y, checker.X,                           //from
                            checker.Y + bound.yDir, checker.X + bound.xDir  //to
                        });
                    }
                }
            }
        }

        public void CheckBoard(Checker checker, Checker[,] board, List<Move> moves)
        {
            foreach (var bound in checker.MoveBounds)
            {
                //make sure the checker is in bounds
                if (bound.yBound(checker.Y) && bound.xBound(checker.X))
                {
                    Checker tile = board[checker.Y + bound.yDir, checker.X + bound.xDir];

                    //leftTile has a checker of the opposite colour on it
                    if (tile != null && tile.Color != checker.Color
                        //make sure we can potentially jump over it
                        && bound.yBound(checker.Y + bound.yDir)
                        && bound.xBound(checker.X + bound.xDir))
                    {
                        if (board[checker.Y + (2 * bound.yDir),
                            checker.X + (2 * bound.xDir)] == null)
                        {
                            moves.Add(new Move(
                                fromY: checker.Y,
                                fromX: checker.X,
                                toY: checker.Y + (2 * bound.yDir),
                                toX: checker.X + (2 * bound.xDir),
                                king: checker.Kingable(checker.Y + (2 * bound.yDir)),
                                capture: true,
                                captureY: checker.Y + bound.yDir,
                                captureX: checker.X + bound.xDir
                                ));
                        }
                    }
                    //check if we can move left
                    else if (tile == null)
                    {
                        moves.Add(new Move(
                                fromY: checker.Y,
                                fromX: checker.X,
                                toY: checker.Y + bound.yDir,
                                toX: checker.X + bound.xDir,
                                king: checker.Kingable(checker.Y + (2 * bound.yDir))
                                ));
                    }
                }
            }
        }

        //Used to add some randomness to decision making
        public bool RandomChance(int percent = 50)
        {
            return random.Next(0, 100) >= percent;
        }

        public int[] MakeMove(Checker[,] board, List<Checker> blacks, List<Checker> reds, Color turn, int depth, int[] currMove)
        {
            int best = 0;
            int curr = 0;
            int[] tempMove = null;
            int[] bestMove = null;
            List<int[]> moves = new List<int[]>();
            Checker toMove;

            Checker[,] newBoard;
            List<Checker> newBlacks;
            List<Checker> newReds;

            bool bestNotSet = true;

            if (depth > 0)
            {
                //GetMoves(board, )
                if (turn == Color.Black)
                {
                    blacks.ForEach(x => CheckBoard(x, board, moves));
                }
                else
                {
                    reds.ForEach(x => CheckBoard(x, board, moves));
                }

                // if there is a capturing move available, remove all non-capturing moves
                if (moves.Any(x => x.Length == 6))
                {
                    moves = moves.Where(x => x.Length == 6).ToList();
                }

                MaxNodes += moves.Count;

                moves.ForEach(move =>
                {
                    if (move.Length >= 4)
                    {
                        newBoard = new Checker[8, 8];
                        newBlacks = blacks.Select(x => new Checker(x)).ToList();
                        newReds = reds.Select(x => new Checker(x)).ToList();

                        newBlacks.ForEach(x => newBoard[x.Y, x.X] = x);
                        newReds.ForEach(x => newBoard[x.Y, x.X] = x);

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

                            //remove the captured checker
                            if (toRemove.Color == Color.Black)
                            {
                                newBlacks.Remove(toRemove);
                            }
                            else
                            {
                                newReds.Remove(toRemove);
                            }
                            newBoard[move[4], move[5]] = null;
                            toRemove = null;
                        }

                        //make the checker a king if possible
                        if (toMove.Kingable(toMove.Y))
                        {
                            toMove.MakeKing();
                        }

                        //evaluate this move
                        tempMove = MakeMove(newBoard, newBlacks, newReds,
                            turn == Color.Black ? Color.Red : Color.Black, depth - 1, move);

                        if (tempMove != null)
                        {
                            //get the score
                            curr = tempMove[tempMove.Length - 1];

                            //update the best move
                            if ((turn == Color.Black && curr > best) ||
                                (turn == Color.Red && curr < best) ||
                                (best == curr && RandomChance()) || bestNotSet)
                            {
                                best = curr;
                                bestMove = new int[move.Length + 1];
                                move.CopyTo(bestMove, 0);
                                bestMove[bestMove.Length - 1] = best;

                                bestNotSet = false;
                            }
                        }
                    }
                });
            }
            else
            {
                bestMove = new int[currMove.Length + 1];
                currMove.CopyTo(bestMove, 0);

                //do this in reverse because recursion
                if (turn == Color.Black)
                {
                    bestMove[bestMove.Length - 1] = RedHeuristic.Calculate(board, blacks, reds);
                }
                else
                {
                    bestMove[bestMove.Length - 1] = BlackHeuristic.Calculate(board, blacks, reds);
                }
            }

            return bestMove;
        }

        public Move MakeMoveClass(Checker[,] board, List<Checker> blacks, List<Checker> reds, Color turn, int depth, Move currMove)
        {
            Move tempMove = null;
            Move bestMove = null;
            List<Move> moves = new List<Move>();
            Checker toMove;

            Checker[,] newBoard;
            List<Checker> newBlacks;
            List<Checker> newReds;

            if (depth > 0)
            {
                //GetMoves(board, )
                if (turn == Color.Black)
                {
                    blacks.ForEach(x => CheckBoard(x, board, moves));
                }
                else
                {
                    reds.ForEach(x => CheckBoard(x, board, moves));
                }

                // if there is a capturing move available, remove all non-capturing moves
                if (moves.Any(x => x.Capture))
                {
                    moves = moves.Where(x => x.Capture).ToList();
                }

                MaxNodes += moves.Count;

                moves.ForEach(move =>
                {
                    newBoard = new Checker[8, 8];
                    newBlacks = blacks.Select(x => new Checker(x)).ToList();
                    newReds = reds.Select(x => new Checker(x)).ToList();

                    newBlacks.ForEach(x => newBoard[x.Y, x.X] = x);
                    newReds.ForEach(x => newBoard[x.Y, x.X] = x);

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

                        //remove the captured checker
                        if (toRemove.Color == Color.Black)
                        {
                            newBlacks.Remove(toRemove);
                        }
                        else
                        {
                            newReds.Remove(toRemove);
                        }
                        newBoard[move.CaptureY, move.CaptureX] = null;
                        toRemove = null;
                    }

                    if (move.King)
                    {
                        toMove.MakeKing();
                    }

                    //evaluate this move
                    tempMove = MakeMoveClass(newBoard, newBlacks, newReds,
                        turn == Color.Black ? Color.Red : Color.Black, depth - 1, move);

                    if (tempMove != null)
                    {
                        if (bestMove == null ||
                            (turn == Color.Black && tempMove.Score > bestMove.Score) ||
                            (turn == Color.Red && tempMove.Score < bestMove.Score) ||
                            (tempMove.Score == bestMove.Score && RandomChance()))
                        {
                            bestMove = new Move(move);
                            bestMove.Score = tempMove.Score;
                        }
                    }
                });
            }
            else
            {
                bestMove = new Move(currMove);

                //do this in reverse because recursion
                if (turn == Color.Black)
                {
                    bestMove.Score = RedHeuristic.Calculate(board, blacks, reds);
                }
                else
                {
                    bestMove.Score = BlackHeuristic.Calculate(board, blacks, reds);
                }
            }

            return bestMove;
        }

        public int[] MakeMoveParallel(Checker[,] board, List<Checker> blacks,
            List<Checker> reds, Color turn, int depth, int[] currMove)
        {
            int best = 0;
            int curr = 0;
            int[] tempMove = null;
            int[] bestMove = null;
            List<int[]> moves = new List<int[]>();
            bool bestNotSet = true;

            if (depth > 0)
            {
                if (turn == Color.Black)
                {
                    blacks.ForEach(x => CheckBoard(x, board, moves));
                }
                else
                {
                    reds.ForEach(x => CheckBoard(x, board, moves));
                }

                // if there is a capturing move available, remove all non-capturing moves
                if (moves.Any(x => x.Length == 6))
                {
                    moves = moves.Where(x => x.Length == 6).ToList();
                }


                var result = new ConcurrentBag<Tuple<int[], int[]>>();
                //Console.WriteLine($"Moves: {moves.Count}");
                Parallel.ForEach(moves, (move) =>
                {
                    if (move.Length >= 4)
                    {
                        Checker toMove;
                        Checker[,] newBoard;
                        List<Checker> newBlacks;
                        List<Checker> newReds;

                        newBoard = new Checker[8, 8];
                        newBlacks = blacks.Select(x => new Checker(x)).ToList();
                        newReds = reds.Select(x => new Checker(x)).ToList();

                        newBlacks.ForEach(x => newBoard[x.Y, x.X] = x);
                        newReds.ForEach(x => newBoard[x.Y, x.X] = x);

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

                            //remove the captured checker
                            if (toRemove.Color == Color.Black)
                            {
                                newBlacks.Remove(toRemove);
                            }
                            else
                            {
                                newReds.Remove(toRemove);
                            }
                            newBoard[move[4], move[5]] = null;
                            toRemove = null;
                        }

                        //make the checker a king if possible
                        if (toMove.Kingable(toMove.Y))
                        {
                            toMove.MakeKing();
                        }

                        //evaluate this move
                        tempMove = MakeMove(newBoard, newBlacks, newReds,
                            turn == Color.Black ? Color.Red : Color.Black, depth - 1, move);
                        result.Add(new Tuple<int[], int[]>(move, tempMove));
                    }
                });

                var moveTuples = result.ToList();

                moveTuples.ForEach(moveTuple =>
                {
                    int[] move = moveTuple.Item1;
                    int[] tempMove = moveTuple.Item2;

                    if (tempMove != null)
                    {
                        //get the score
                        curr = tempMove[tempMove.Length - 1];

                        //update the best move
                        if ((turn == Color.Black && curr > best) ||
                            (turn == Color.Red && curr < best) ||
                            (best == curr && RandomChance()) || bestNotSet)
                        {
                            best = curr;
                            bestMove = new int[move.Length + 1];
                            move.CopyTo(bestMove, 0);
                            bestMove[bestMove.Length - 1] = best;

                            bestNotSet = false;
                        }
                    }
                });
            }
            else
            {
                bestMove = new int[currMove.Length + 1];
                currMove.CopyTo(bestMove, 0);

                //do this in reverse because recursion
                if (turn == Color.Black)
                {
                    bestMove[bestMove.Length - 1] = RedHeuristic.Calculate(board, blacks, reds);
                }
                else
                {
                    bestMove[bestMove.Length - 1] = BlackHeuristic.Calculate(board, blacks, reds);
                }
            }

            return bestMove;
        }

        public void CompareSpeed()
        {
            PlayTurn(Color.Black, MakeMove);
            Reset();
            PlayTurn(Color.Black, MakeMoveParallel);
            Reset();
            PlayTurnClass(Color.Black, MakeMoveClass);
            //PlayTurn2(Color.Black, MakeMove2);
        }

        public bool GameOver(object move, int turn)
        {
            string output = "\n\n";
            bool gameOver = false;

            if (Blacks.Count == 0 || (move == null && Reds.Count > Blacks.Count))
            {
                output = $"Reds win on turn {turn}";
                gameOver = true;
            }
            else if (Reds.Count == 0 || (move == null && Blacks.Count > Reds.Count))
            {
                output = $"Blacks win on turn {turn}";
                gameOver = true;
            }
            else if (move == null)
            {
                output = $"Tie on turn {turn}";
                gameOver = true;
            }

            if (gameOver)
            {
                Watch.Stop();

                output += $" in {Watch.ElapsedMilliseconds}ms\n" +
                    $"MaxNodes {MaxNodes}";

                PrintBoard();
                Console.WriteLine(output);
            }

            return gameOver;
        }

        public void PlayTurn(Color color, Func<Checker[,], List<Checker>,
            List<Checker>, Color, int, int[], int[]> GetMove)
        {
            int[] move = null;
            Checker toMove;
            //int turn = 0;
            int depth = 1;

            Watch.Start();

            for(int turn = 0; ; turn++)
            {
                PrintBoard(color, move?[move.Length == 5 ? 4 : 6]);

                if(turn % 100 == 0)
                {
                    depth++;
                }

                move = GetMove(Board, Blacks, Reds, color, depth, null);

                if (move != null && move.Length > 4)
                {
                    toMove = Board[move[0], move[1]];
                    //update board
                    Board[move[2], move[3]] = toMove;
                    Board[move[0], move[1]] = null;

                    //update toMove
                    toMove.Y = move[2];
                    toMove.X = move[3];

                    if (move.Length >= 6)
                    {
                        Checker toRemove = Board[move[4], move[5]];

                        //remove the captured checker
                        if (toRemove.Color == Color.Black)
                        {
                            Blacks.Remove(toRemove);
                        }
                        else
                        {
                            Reds.Remove(toRemove);
                        }
                        Board[move[4], move[5]] = null;
                    }

                    //make the checker a king if possible
                    if (toMove.Kingable(toMove.Y))
                    {
                        toMove.MakeKing();
                    }
                }

                if (GameOver(move, turn))
                {
                    break;
                }

                //change color
                color = color == Color.Black ? Color.Red : Color.Black;
            }
        }

        public void PlayTurnClass(Color color, Func<Checker[,], List<Checker>,
            List<Checker>, Color, int, Move, Move> GetMove)
        {
            Move move = null;
            Checker toMove;
            int turn = 0;
            int depth = 6;

            Watch.Start();

            while (true)
            {
                PrintBoard(color, move?.Score);
                move = GetMove(Board, Blacks, Reds, color, depth, null);

                if (move != null)
                {
                    toMove = Board[move.FromY, move.FromX];
                    //update board
                    Board[move.ToY, move.ToX] = toMove;
                    Board[move.FromY, move.FromX] = null;

                    //update toMove
                    toMove.Y = move.ToY;
                    toMove.X = move.ToX;

                    if (move.Capture)
                    {
                        Checker toRemove = Board[move.CaptureY, move.CaptureX];

                        //remove the captured checker
                        if (toRemove.Color == Color.Black)
                        {
                            Blacks.Remove(toRemove);
                        }
                        else
                        {
                            Reds.Remove(toRemove);
                        }
                        Board[move.CaptureY, move.CaptureX] = null;
                    }

                    if (move.King)
                    {
                        toMove.MakeKing();
                    }
                }

                if (GameOver(move, turn))
                {
                    break;
                }

                //change color
                color = color == Color.Black ? Color.Red : Color.Black;
                turn++;
            }
        }
    }
}
