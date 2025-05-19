using System;
using System.Collections.Generic;

namespace SnakeGame1
{
    class Walls
    {
        List<Point> wallList; // Список всех точек стены

        // Конструктор, отрисовывающий рамку по периметру поля
        public Walls(int mapWidth, int mapHeight)
        {
            wallList = new List<Point>();

            // Верхняя и нижняя границы
            for (int x = 0; x < mapWidth; x++)
            {
                wallList.Add(new Point(x, 0, '#', ConsoleColor.Gray));
                wallList.Add(new Point(x, mapHeight - 1, '#', ConsoleColor.Gray));
            }

            // Левая и правая границы
            for (int y = 0; y < mapHeight; y++)
            {
                wallList.Add(new Point(0, y, '#', ConsoleColor.Gray));
                wallList.Add(new Point(mapWidth - 1, y, '#', ConsoleColor.Gray));
            }
        }

        // Отрисовка стен
        public void Draw()
        {
            foreach (var wall in wallList)
            {
                wall.Draw();
            }
        }

        // Проверка, врезалась ли змейка в стену
        public bool IsHit(Point p)
        {
            foreach (var wall in wallList)
            {
                if (wall.IsHit(p))
                    return true;
            }
            return false;
        }
    }
}
