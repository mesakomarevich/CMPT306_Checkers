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

            BlackHeuristic = Heuristic.DefaultHeuristic(Color.Black);
            RedHeuristic = Heuristic.DefaultHeuristic(Color.Red);
        }

        public void PrintBoard(int? turnNumber = null, Color? turn = null, int? score = null)
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
                Console.WriteLine("Turn {0}: {1}", turnNumber, turn == Color.Black ? "Black" : "Red");
                Console.WriteLine($"Average Turn Time: {Watch.ElapsedMilliseconds / turnNumber}ms");
                Console.WriteLine($"Time: {Watch.ElapsedMilliseconds}ms");
                Console.WriteLine($"Moves Enumerated: {MaxNodes}");
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

        public GameState MakeMove(GameState gameState)
        {
            bool bestNotSet = true;
            int best = 0;
            int curr = 0;
            int[] bestMove = null;
            List<int[]> moves = new List<int[]>();
            GameState tempMove = null;
            
            if (gameState.Depth > 0)
            {
                if (gameState.Turn == Color.Black)
                {
                    gameState.Blacks.ForEach(x => CheckBoard(x, gameState.Board, moves));
                }
                else
                {
                    gameState.Reds.ForEach(x => CheckBoard(x, gameState.Board, moves));
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
                        //evaluate this move
                        tempMove = MakeMove(gameState.ApplyMove(move));
                        //Console.WriteLine($"Move length {tempMove.Move.Length}");
                        if (tempMove.Move != null)
                        {
                            //get the score
                            curr = tempMove.Move[tempMove.Move.Length - 1];

                            //update the best move
                            if ((gameState.Turn == Color.Black && curr > best) ||
                                (gameState.Turn == Color.Red && curr < best) ||
                                (best == curr && RandomChance()) || bestNotSet)
                            {
                                best = curr;
                                bestMove = new int[move.Length + 1];
                                //copy the checker movements
                                move.CopyTo(bestMove, 0);
                                //set score
                                bestMove[bestMove.Length - 1] = best;

                                gameState.Move = bestMove;
                                bestNotSet = false;
                            }
                        }
                    }
                });
            }
            else
            {
                bestMove = new int[gameState.Move.Length + 1];
                gameState.Move.CopyTo(bestMove, 0);

                bestMove[bestMove.Length - 1] = gameState.Turn == Color.Black
                    ? RedHeuristic.Calculate(gameState.Board, gameState.Blacks, gameState.Reds)
                    : BlackHeuristic.Calculate(gameState.Board, gameState.Blacks, gameState.Reds);

                gameState.Move = bestMove;
            }

            //Console.WriteLine($"gameState return move length {gameState.Move?.Length}");
            return gameState;
            //return bestMove;
        }

        public GameStateClass MakeMoveClass(GameStateClass gameState)
        {
            GameStateClass tempMove = null;
            Move bestMove = null;
            List<Move> moves = new List<Move>();

            if (gameState.Depth > 0)
            {
                //GetMoves(board, )
                if (gameState.Turn == Color.Black)
                {
                    gameState.Blacks.ForEach(x => CheckBoard(x, gameState.Board, moves));
                }
                else
                {
                    gameState.Reds.ForEach(x => CheckBoard(x, gameState.Board, moves));
                }

                // if there is a capturing move available, remove all non-capturing moves
                if (moves.Any(x => x.Capture))
                {
                    moves = moves.Where(x => x.Capture).ToList();
                }

                MaxNodes += moves.Count;

                moves.ForEach(move =>
                {
                    tempMove = gameState.ApplyMove(move);

                    //evaluate this move
                    tempMove = MakeMoveClass(tempMove);

                    if (tempMove != null)
                    {
                        if (bestMove == null ||
                            (gameState.Turn == Color.Black && tempMove.Move.Score > bestMove.Score) ||
                            (gameState.Turn == Color.Red && tempMove.Move.Score < bestMove.Score) ||
                            (tempMove.Move.Score == bestMove.Score && RandomChance()))
                        {
                            gameState.Move = new Move(move);
                            gameState.Move.Score = tempMove.Move.Score;
                        }
                    }
                });
            }
            else
            {
                //bestMove = new Move(gameState.Move);

                //do this in reverse because recursion
                if (gameState.Turn == Color.Black)
                {
                    gameState.Move.Score = RedHeuristic.Calculate(gameState.Board, gameState.Blacks, gameState.Reds);
                }
                else
                {
                    gameState.Move.Score = BlackHeuristic.Calculate(gameState.Board, gameState.Blacks, gameState.Reds);
                }
            }

            return gameState;
        }

        public GameState MakeMoveParallel(GameState gameState)
        {
            int best = 0;
            int curr = 0;
            GameState tempMove = null;
            int[] bestMove = null;
            List<int[]> moves = new List<int[]>();
            bool bestNotSet = true;

            if (gameState.Depth > 0)
            {
                if (gameState.Turn == Color.Black)
                {
                    gameState.Blacks.ForEach(x => CheckBoard(x, gameState.Board, moves));
                }
                else
                {
                    gameState.Reds.ForEach(x => CheckBoard(x, gameState.Board, moves));
                }

                // if there is a capturing move available, remove all non-capturing moves
                if (moves.Any(x => x.Length == 6))
                {
                    moves = moves.Where(x => x.Length == 6).ToList();
                }

                MaxNodes += moves.Count;


                var result = new ConcurrentBag<Tuple<int[], GameState>>();
                //Console.WriteLine($"Moves: {moves.Count}");
                Parallel.ForEach(moves, (move) =>
                {
                    if (move.Length >= 4)
                    {
                        tempMove = MakeMove(gameState.ApplyMove(move));
                        result.Add(new Tuple<int[], GameState>(move, tempMove));
                    }
                });

                var moveTuples = result.ToList();

                moveTuples.ForEach(moveTuple =>
                {
                    int[] move = moveTuple.Item1;
                    GameState tempMove = moveTuple.Item2;

                    if (tempMove.Move != null)
                    {
                        //get the score
                        curr = tempMove.Move[tempMove.Move.Length - 1];

                        //update the best move
                        if ((gameState.Turn == Color.Black && curr > best) ||
                            (gameState.Turn == Color.Red && curr < best) ||
                            (best == curr && RandomChance()) || bestNotSet)
                        {
                            best = curr;
                            bestMove = new int[move.Length + 1];
                            //copy the checker movements
                            move.CopyTo(bestMove, 0);
                            //set score
                            bestMove[bestMove.Length - 1] = best;

                            gameState.Move = bestMove;
                            bestNotSet = false;
                        }
                    }
                });
            }
            else
            {
                bestMove = new int[gameState.Move.Length + 1];
                gameState.Move.CopyTo(bestMove, 0);

                bestMove[bestMove.Length - 1] = gameState.Turn == Color.Black
                    ? RedHeuristic.Calculate(gameState.Board, gameState.Blacks, gameState.Reds)
                    : BlackHeuristic.Calculate(gameState.Board, gameState.Blacks, gameState.Reds);

                gameState.Move = bestMove;
            }

            return gameState;
        }


        public void CompareSpeed()
        {
            Console.WriteLine("Press Enter to Play MiniMax Game with int[] to store moves");
            Console.ReadLine();
            PlayTurn(Color.Black, MakeMove);
            Reset();
            //Console.WriteLine("Press any key to continue");
            //Console.Read();
            Console.WriteLine("\n\n\nPress Enter to Play MiniMax Game with int[] to store moves and multiple threads");
            Console.ReadLine();
            PlayTurn(Color.Black, MakeMoveParallel);
            Reset();
            //Console.WriteLine("Press any key to continue");
            //Console.Read();
            Console.WriteLine("\n\n\nPress Enter to Play MiniMax Game with move objects to store moves");
            Console.ReadLine();
            PlayTurnClass(Color.Black, MakeMoveClass);
            //PlayTurn2(Color.Black, MakeMove2);
            Reset();
            
            PlayGame();
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

                //PrintBoard(turn, null, null);
                Console.WriteLine(output);
            }

            return gameOver;
        }

        public void PlayGame(int depth = 4)
        {
            GameState p1State = null;
            GameStateClass p2State = null;

            int? score = null;
            Color color = Color.Black;
            Color p1;
            Color p2;
            string p1Color;
            string p2Color;
            if (RandomChance())
            {
                p1 = Color.Black;
                p1Color = "Black";
                p2 = Color.Red;
                p2Color = "Red";
            }
            else
            {
                p2 = Color.Black;
                p2Color = "Black";
                p1 = Color.Red;
                p1Color = "Red";
            }
            Console.WriteLine($"\n\n\nPress Enter to Play MiniMax Game with the {p1Color} Checkers using " +
                $"multiple threads and the {p2Color} checkers using a single thread");
            Console.ReadLine();

            Watch.Start();
            //PrintBoard(1, color, score);
            Stopwatch p1watch = new Stopwatch();
            Stopwatch p2watch = new Stopwatch();

            int p1Turns = 0;
            int p2Turns = 0;
            int p1Moves = 0;
            int p2Moves = 0;

            for (int turn = 1; ; turn++)
            {
                if (turn % 100 == 0)
                {
                    depth++;
                }
                if (color == p1)
                {
                    p1watch.Start();
                    int movesStart = MaxNodes;
                    p1State = MakeMoveParallel(new GameState(Board, Blacks, Reds, p1, depth, null)).ApplyMove();
                    Board = p1State.Board;
                    Blacks = p1State.Blacks;
                    Reds = p1State.Reds;
                    score = p1State?.Move[p1State.Move.Length == 5 ? 4 : 6];

                    int movesEnd = MaxNodes;
                    p1Moves += (movesEnd - movesStart);
                    p1Turns++;
                    if (GameOver(p1State.Move, turn))
                    {
                        PrintBoard(turn, color, score);
                        break;
                    }
                    p1watch.Stop();
                }
                else
                {
                    p2watch.Start();
                    int movesStart = MaxNodes;
                    p2State = MakeMoveClass(new GameStateClass(Board, Blacks, Reds, p2, depth, null)).ApplyMove();
                    Board = p2State.Board;
                    Blacks = p2State.Blacks;
                    Reds = p2State.Reds;
                    score = p2State?.Move?.Score;

                    int movesEnd = MaxNodes;
                    p2Moves += (movesEnd - movesStart);
                    p2Turns++;
                    if (GameOver(p2State.Move, turn))
                    {
                        PrintBoard(turn, color, score);
                        break;
                    }
                    p2watch.Stop();
                }
                PrintBoard(turn, color, score);
                //change color
                color = color == Color.Black ? Color.Red : Color.Black;
            }
            

            Console.WriteLine($"{p1Color} time: {p1watch.ElapsedMilliseconds}ms");
            Console.WriteLine($"{p1Color} time per turn: {p1watch.ElapsedMilliseconds/p1Turns}ms");
            Console.WriteLine($"{p1Color} move enumerated: {p1Moves}");

            Console.WriteLine($"{p2Color} time: {p2watch.ElapsedMilliseconds}ms");
            Console.WriteLine($"{p2Color} time per turn: {p2watch.ElapsedMilliseconds / p2Turns}ms");
            Console.WriteLine($"{p2Color} move enumerated: {p2Moves}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="GetMove"></param>
        public void PlayTurn(Color color, Func<GameState, GameState> GetMove, int depth = 4)
        {
            GameState gameState = null;

            Watch.Start();

            for (int turn = 1; ; turn++)
            {
                if (turn % 100 == 0)
                {
                    depth++;
                }

                gameState = GetMove(new GameState(Board, Blacks, Reds, color, depth, null)).ApplyMove();
                Board = gameState.Board;
                Blacks = gameState.Blacks;
                Reds = gameState.Reds;
                
                if (GameOver(gameState.Move, turn))
                {
                    PrintBoard(turn, color, gameState?.Move[gameState.Move.Length == 5 ? 4 : 6]);
                    break;
                }

                PrintBoard(turn, color, gameState?.Move[gameState.Move.Length == 5 ? 4 : 6]);
                //change color
                color = color == Color.Black ? Color.Red : Color.Black;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="GetMove"></param>
        public void PlayTurnClass(Color color, Func<GameStateClass, GameStateClass> GetMove, int depth = 4)
        {
            GameStateClass gameState = null;

            Watch.Start();

            for (int turn = 1; ; turn++)
            {
                if (turn % 100 == 0)
                {
                    depth++;
                }

                gameState = GetMove(new GameStateClass(Board, Blacks, Reds, color, depth, null)).ApplyMove();
                Board = gameState.Board;
                Blacks = gameState.Blacks;
                Reds = gameState.Reds;

                if (GameOver(gameState.Move, turn))
                {
                    PrintBoard(turn, color, gameState?.Move?.Score);
                    break;
                }
                PrintBoard(turn, color, gameState?.Move?.Score);
                //change color
                color = color == Color.Black ? Color.Red : Color.Black;
            }
        }
    }
}
