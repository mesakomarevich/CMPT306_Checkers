using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

            Console.WriteLine($"Blacks: {Blacks.Count}");
            Console.WriteLine($"Reds: {Reds.Count}");
        }


        public int BlackHeuristic(Checker[,] board, List<Checker> blacks, List<Checker> reds)
        {
            int win = 10000;
            int capture = 100;
            int distanceMult = 20;

            int score = 0;

            score += blacks.Aggregate(0, 
                (bscore, black) => bscore += reds.Aggregate(0, 
                    (rscore, red) => 
                            rscore += distanceMult - (Math.Abs(black.X - red.X) + Math.Abs(black.Y - red.Y))));

            if(reds.Count != 0)
            {
                score += ((12 - reds.Count) * capture);
            }
            else
            {
                score += win;
            }
            

            return score;
        }

        public int RedHeuristic(Checker[,] board, List<Checker> blacks, List<Checker> reds)
        {
            int win = -10000;
            int capture = -100;
            int distanceMult = -20;
            int score = 0;

            score += reds.Aggregate(0,
                (rscore, red) => rscore += blacks.Aggregate(0,
                    (bscore, black) =>
                            bscore += distanceMult - (Math.Abs(red.X - black.X) + Math.Abs(red.Y - black.Y))));

            if(blacks.Count != 0)
            {
                score += ((12 - blacks.Count) * capture);
            }
            else
            {
                score += win;
            }

            return score;
        }


        public void CheckUpLeft(Checker checker, Checker[,] board, List<int[]> moves)
        {
            if (checker.Y < 7 && checker.X > 0)
            {
                Checker leftTile = board[checker.Y + 1, checker.X - 1];

                //leftTile has a checker of the opposite colour on it
                if (leftTile != null && leftTile.Color != checker.Color
                    //make sure we can potentially jump over it
                    && checker.Y + 1 < 7 && checker.X - 1 > 0)
                {
                    if (board[checker.Y + 2, checker.X - 2] == null)
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

        public void CheckUpRight(Checker checker, Checker[,] board, List<int[]> moves)
        {
            if (checker.Y < 7 && checker.X < 7)
            {
                Checker rightTile = board[checker.Y + 1, checker.X + 1];

                //leftTile has a checker of the opposite colour on it
                if (rightTile != null && rightTile.Color != checker.Color
                    //make sure we can potentially jump over it
                    && checker.Y + 1 < 7 && checker.X + 1 < 7)
                {
                    if (board[checker.Y + 2, checker.X + 2] == null)
                    {
                        moves.Add(new int[] {
                            checker.Y, checker.X,           //from
                            checker.Y + 2, checker.X + 2,   //to
                            checker.Y + 1, checker.X + 1 });//capture
                    }
                }
                //check if we can move left
                else if (rightTile == null)
                {
                    moves.Add(new int[]
                    {
                        checker.Y, checker.X,           //from
                        checker.Y + 1, checker.X + 1    //to
                    });
                }
            }
        }

        public void CheckDownLeft(Checker checker, Checker[,] board, List<int[]> moves)
        {
            if (checker.Y > 0 && checker.X > 0)
            {
                Checker leftTile = board[checker.Y - 1, checker.X - 1];

                //leftTile has a checker of the opposite colour on it
                if (leftTile != null && leftTile.Color != checker.Color
                    //make sure we can potentially jump over it
                    && checker.Y - 1 > 0 && checker.X - 1 > 0)
                {
                    if (board[checker.Y - 2, checker.X - 2] == null)
                    {
                        moves.Add(new int[] {
                            checker.Y, checker.X,           //from
                            checker.Y - 2, checker.X - 2,   //to
                            checker.Y - 1, checker.X - 1 });//capture
                    }
                }
                //check if we can move left
                else if (leftTile == null)
                {
                    moves.Add(new int[]
                    {
                        checker.Y, checker.X,           //from
                        checker.Y -1, checker.X - 1    //to
                    });
                }
            }
        }

        public void CheckDownRight(Checker checker, Checker[,] board, List<int[]> moves)
        {
            if (checker.Y > 0 && checker.X < 7)
            {
                Checker rightTile = board[checker.Y - 1, checker.X + 1];

                //leftTile has a checker of the opposite colour on it
                if (rightTile != null && rightTile.Color != checker.Color
                    //make sure we can potentially jump over it
                    && checker.Y - 1 > 0 && checker.X + 1 < 7)
                {
                    if (board[checker.Y - 2, checker.X + 2] == null)
                    {
                        moves.Add(new int[] {
                            checker.Y, checker.X,           //from
                            checker.Y - 2, checker.X + 2,   //to
                            checker.Y - 1, checker.X + 1 });//capture
                    }
                }
                //check if we can move left
                else if (rightTile == null)
                {
                    moves.Add(new int[]
                    {
                        checker.Y, checker.X,           //from
                        checker.Y - 1, checker.X + 1    //to
                    });
                }
            }
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
                if (turn == Color.Black)
                {
                    foreach (var black in blacks)
                    {
                        CheckUpLeft(black, board, moves);
                        CheckUpRight(black, board, moves);
                    }
                }
                else
                {
                    foreach (var red in reds)
                    {
                        CheckDownLeft(red, board, moves);
                        CheckDownRight(red, board, moves);
                    }
                }

                // if there is a capturing move available, remove all non-capturing moves
                if(moves.Any(x => x.Length == 6))
                {
                    moves = moves.Where(x => x.Length == 6).ToList();
                }

                moves.ForEach(move =>
                {
                    if (move.Length >= 4)
                    {
                        newBoard = new Checker[8,8];
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

                        //evaluate this move
                        tempMove = MakeMove(newBoard, newBlacks, newReds, turn == Color.Black ? Color.Red : Color.Black, depth - 1, move);

                        if(tempMove != null)
                        {
                            //get the score
                            curr = tempMove[tempMove.Length - 1];
                            //black wants to maximize
                            if (turn == Color.Black)
                            {
                                if (curr > best || bestNotSet)
                                {
                                    best = curr;
                                    bestMove = new int[move.Length + 1];
                                    move.CopyTo(bestMove, 0);
                                    bestMove[bestMove.Length - 1] = best;

                                    //bestMove = tempMove;
                                    bestNotSet = false;
                                }
                            }
                            //red wants to minimize
                            else
                            {
                                if (curr < best || bestNotSet)
                                {
                                    best = curr;
                                    bestMove = new int[move.Length + 1];
                                    move.CopyTo(bestMove, 0);
                                    bestMove[bestMove.Length - 1] = best;

                                    //bestMove = tempMove;
                                    bestNotSet = false;
                                }
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
                    bestMove[bestMove.Length - 1] = RedHeuristic(board, blacks, reds);
                }
                else
                {
                    bestMove[bestMove.Length - 1] = BlackHeuristic(board, blacks, reds);
                }
            }

            return bestMove;
        }

        public int[] MakeMoveParallel(Checker[,] board, List<Checker> blacks, List<Checker> reds, Color turn, int depth, int[] currMove)
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
                    foreach (var black in blacks)
                    {
                        CheckUpLeft(black, board, moves);
                        CheckUpRight(black, board, moves);
                    }
                }
                else
                {
                    foreach (var red in reds)
                    {
                        CheckDownLeft(red, board, moves);
                        CheckDownRight(red, board, moves);
                    }
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

                        //evaluate this move
                        tempMove = MakeMove(newBoard, newBlacks, newReds, turn == Color.Black ? Color.Red : Color.Black, depth - 1, move);
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
                        //black wants to maximize
                        if (turn == Color.Black)
                        {
                            if (curr > best || bestNotSet)
                            {
                                best = curr;
                                bestMove = new int[move.Length + 1];
                                move.CopyTo(bestMove, 0);
                                bestMove[bestMove.Length - 1] = best;

                                //bestMove = tempMove;
                                bestNotSet = false;
                            }
                        }
                        //red wants to minimize
                        else
                        {
                            if (curr < best || bestNotSet)
                            {
                                best = curr;
                                bestMove = new int[move.Length + 1];
                                move.CopyTo(bestMove, 0);
                                bestMove[bestMove.Length - 1] = best;

                                //bestMove = tempMove;
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
                    bestMove[bestMove.Length - 1] = RedHeuristic(board, blacks, reds);
                }
                else
                {
                    bestMove[bestMove.Length - 1] = BlackHeuristic(board, blacks, reds);
                }
            }

            return bestMove;
        }

        public void CompareSpeed()
        {
            PlayTurn(Color.Black, MakeMove);
            Reset();
            PlayTurn(Color.Black, MakeMoveParallel);
        }

        public void PlayTurn(Color color, Func<Checker[,], List<Checker>, List<Checker>, Color, int, int[], int[]> GetMove)
        {
            int[] move = null;
            Checker toMove;
            int turn = 0;
            int depth = 6;
            Stopwatch watch = new Stopwatch();
            string output = "";

            watch.Start();


            while (true)
            {
                //PrintBoard();

                //Console.ReadLine();
                //move = MakeMove(Board, Blacks, Reds, color, depth, null);
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
                        toRemove = null;
                    }
                }

                if(Blacks.Count == 0 || (move == null && Reds.Count > Blacks.Count))
                {
                    output = $"Reds win on turn {turn}";
                    break;
                }
                else if (Reds.Count == 0 || (move == null && Blacks.Count > Reds.Count))
                {
                    output = $"Blacks win on turn {turn}";
                    break;
                }
                else if(move == null)
                {
                    output = $"Tie on turn {turn}";
                    break;
                }

                //change color
                color = color == Color.Black ? Color.Red : Color.Black;
                turn++;
            }

            watch.Stop();

            output += $" in {watch.ElapsedMilliseconds}ms";

            PrintBoard();
            Console.WriteLine(output);

        }
    }
}
