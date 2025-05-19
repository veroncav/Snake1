using System;
using System.Collections.Generic;

namespace SnakeGame1
{
    class FoodCreator
    {
        private int x;
        private int y;
        private char symbol; // Символ, обозначающий еду ('$' или 'X')
        private ConsoleColor color; // Цвет еды
        private Random random = new Random();
        public bool IsBonus { get; private set; } // Флаг, бонусная ли еда
        private DateTime createdTime; // Время появления еды

        // Конструктор с размерами поля
        public FoodCreator(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Метод создания еды. Если bonus = true — создаём X (бонус), иначе обычную еду
        public Point CreateFood(bool bonus = false)
        {
            int foodX = random.Next(2, x - 2);
            int foodY = random.Next(2, y - 2);
            IsBonus = bonus;
            symbol = bonus ? 'X' : '$';
            color = bonus ? ConsoleColor.Red : ConsoleColor.Yellow;
            createdTime = DateTime.Now;

            Point food = new Point(foodX, foodY, symbol, color);
            food.Draw();
            return food;
        }

        // Проверяем, не просрочена ли бонусная еда (10 секунд)
        public bool IsExpired()
        {
            return IsBonus && (DateTime.Now - createdTime).TotalSeconds > 10;
        }
    }
}