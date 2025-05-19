using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SnakeGame1;

namespace SnakeGame1
{ 
    internal class HorizontalLine : Figure
    {


        public HorizontalLine(int xLeft, int xRight, int y, char sym)
        {
            pList = new List<Point>();

            for (int x = xLeft; x <= xRight; x++)
            {
                Point p = new Point(x, y, sym, ConsoleColor.Green);
                pList.Add(p);
            }
        }
    }
}