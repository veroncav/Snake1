using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameSnake
{
    internal class Figure
    {
        protected List<Point> pList;
        public List<Point> GetPoints()
        {
            return pList;
        }
        public void Draw()
        {
            foreach (Point p in pList)
            {
                p.Draw();
            }
        }

        public void Clear()
        {
            foreach (Point p in pList)
            {
                p.Clear();
            }
        }

        internal bool IsHit(Figure other)
        {
            foreach (var p in pList)
            {
                if (other.IsHit(p))
                    return true;
            }
            return false;
        }

        private bool IsHit(Point point)
        {
            foreach (var p in pList)
            {
                if (p.IsHit(point))
                    return true;
            }
            return false;
        }
    }
}
