using System;
using System.Runtime.InteropServices;

namespace CMPT306_Checkers
{
    public struct c2
    {
        public int Id { get; set; }

        public Color Color { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool King { get; set; }


        public c2(int newId, Color newColor, int newX, int newY, bool newKing)
        {
            Id = newId;
            Color = newColor;
            X = newX;
            Y = newY;
            King = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Game game = new Game();

            //game.PrintBoard();

            //Console.WriteLine("Checker size: " + Marshal.SizeOf(c));
            //DisplaySizeOf<Game>();
            //game.PlayTurn(Color.Black);
            game.CompareSpeed();
        }
    }
}
