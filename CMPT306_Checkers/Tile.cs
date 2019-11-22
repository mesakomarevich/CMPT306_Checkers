using System;
namespace CMPT306_Checkers
{
    public class Tile
    {
        public Color Color { get; set; }

        public Checker Checker { get; set; }

        public Tile()
        {
        }

        public Tile(Color newColor) : this()
        {
            Color = newColor;
        }

        public Tile(Color newColor, Checker newChecker) : this()
        {
            Checker = newChecker;
        }
    }
}
