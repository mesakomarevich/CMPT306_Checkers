using System;
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

        public Checker(int newId, Color newColor, int newX, int newY) : this()
        {
            Id = newId;
            Color = newColor;
            X = newX;
            Y = newY;
        }
    }

    public enum Color
    {
        Black = 0,
        Red = 1
    }
}