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
            
            //game.CompareSpeed();
            game.Benchmark();

            //for(int i = 0; i < 50; i++)
            //{
            //    game.PlayTurnClass()
            //}
        }
    }
}
