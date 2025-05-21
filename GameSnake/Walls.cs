using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace GameSnake
{
    internal class Walls : Figure
    {
        public Walls(int mapWidth, int mapHeight)
        {
            pList = new List<Point>();

            // Создаём верхнюю горизонтальную линию
            HorizontalLine topLine = new HorizontalLine(0, mapWidth - 1, 0, '#', ConsoleColor.Gray);
            // Создаём нижнюю горизонтальную линию
            HorizontalLine bottomLine = new HorizontalLine(0, mapWidth - 1, mapHeight - 1, '#', ConsoleColor.Gray);
            // Создаём левую вертикальную линию
            VerticalLine leftLine = new VerticalLine(0, mapHeight - 1, 0, '#', ConsoleColor.Gray);
            // Создаём правую вертикальную линию
            VerticalLine rightLine = new VerticalLine(0, mapHeight - 1, mapWidth - 1, '#', ConsoleColor.Gray);

            // Добавляем точки всех линий в список точек стен
            pList.AddRange(topLine.GetPoints());
            pList.AddRange(bottomLine.GetPoints());
            pList.AddRange(leftLine.GetPoints());
            pList.AddRange(rightLine.GetPoints());
        }

        // Метод проверяет, столкнулась ли точка с любой из стен
        public bool IsHit(Point p)
        {
            foreach (var wallPoint in pList)
            {
                if (wallPoint.IsHit(p))
                    return true;
            }
            return false;
        }
    }
}
