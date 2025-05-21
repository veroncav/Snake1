using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSnake
{
    internal class FoodCreator
    {
        private int mapWidth;
        private int mapHeight;
        private char foodSymbol;
        private ConsoleColor foodColor;
        private Random random;

        public FoodCreator(int mapWidth, int mapHeight, char foodSymbol, ConsoleColor foodColor)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.foodSymbol = foodSymbol;
            this.foodColor = foodColor;
            random = new Random();
        }

        public Point CreateFood(Snake snake)
        {
            Point foodPoint;
            do
            {
                int x = random.Next(1, mapWidth - 1);
                int y = random.Next(1, mapHeight - 1);
                foodPoint = new Point(x, y, foodSymbol, foodColor);
            } while (snake.IsHit(foodPoint)); // Проверяем, чтобы еда не появилась в теле змейки

            return foodPoint;
        }
    }
}
