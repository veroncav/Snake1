using System;

namespace Snakegame
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }

        public Point(int x, int y, char symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }

        public void Draw()
        {
            if (X >= 0 && X < Console.WindowWidth && Y >= 0 && Y < Console.WindowHeight)
            {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Symbol);
                Console.ResetColor();
            }
        }

        public void Clear()
        {
            if (X >= 0 && X < Console.WindowWidth && Y >= 0 && Y < Console.WindowHeight)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(' ');
            }
        }

        public void Move(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public bool IsHit(Point other)
        {
            return other != null && X == other.X && Y == other.Y;
        }

        public Point Clone()
        {
            return new Point(X, Y, Symbol);
        }
    }
}
