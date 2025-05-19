using System;

namespace SnakeGame1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем объект игры
            SnakeMadu game = new SnakeMadu();
            game.ShowStartupOptions();

            // Здесь позже будет логика запуска Walls, Snake и т.д.
            Console.WriteLine("Настройки выбраны. Запуск игры будет здесь.");
        }
    }
}

