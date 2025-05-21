using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSnake
{
    internal class VerticalLine : Figure
    {
        public VerticalLine(int yTop, int yBottom, int x, char sym, ConsoleColor color)
        {
            pList = new List<Point>();
            for (int y = yTop; y <= yBottom; y++)
            {
                pList.Add(new Point(x, y, sym, color));
            }
        }
    }
}

