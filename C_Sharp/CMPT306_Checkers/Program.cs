using System;
using System.Runtime.InteropServices;

namespace CMPT306_Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Game game = new Game();

            //game.PrintBoard();

            //Console.WriteLine($"black score {game.BlackHeuristic.Calculate(game.Board, game.Blacks, game.Reds)}");
            //Console.WriteLine($"red score {game.RedHeuristic.Calculate(game.Board, game.Blacks, game.Reds)}");

            //Console.WriteLine("Checker size: " + Marshal.SizeOf(c));
            //DisplaySizeOf<Game>();
            //game.PlayTurn(Color.Black);
            game.CompareSpeed();
        }
    }
}
