using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSnake
{
    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Point(int x, int y, char symbol, ConsoleColor color = ConsoleColor.Green)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
            Console.ResetColor();
        }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }

        public bool IsHit(Point other)
        {
            return other.X == X && other.Y == Y;
        }

        public Point Copy()
        {
            return new Point(X, Y, Symbol, Color);
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Symbol}";
        }
    }
}
