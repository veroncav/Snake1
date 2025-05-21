using System;
using System.Threading;

namespace SnakeGame1
{
  
    class Program
    {
        static void Main(string[] args)
        {
            SnakeGame game = new SnakeGame();
            game.ShowStartupOptions();
            game.RunGame();

            while (game.AskRestart())
            {
                game = new SnakeGame();
                game.ShowStartupOptions();
                game.RunGame();
            }
        }
    }

    class SnakeGame
    {
        private int score;
        private SpeedLevel speed;
        private ConsoleColor snakeColor;
        private ConsoleColor backgroundColor;

        public SnakeGame()
        {
            score = 0;
            speed = SpeedLevel.Normal;
            snakeColor = ConsoleColor.Green;
            backgroundColor = ConsoleColor.Black;
        }

        public void ShowStartupOptions()
        {
            Console.Clear();
            Console.WriteLine("Выберите скорость:");
            Console.WriteLine("1 - Медленно\n2 - Нормально\n3 - Быстро");

            ConsoleKey speedKey = Console.ReadKey(true).Key;
            if (speedKey == ConsoleKey.D1) speed = SpeedLevel.Slow;
            else if (speedKey == ConsoleKey.D3) speed = SpeedLevel.Fast;
            else speed = SpeedLevel.Normal;

            Console.Clear();
            Console.WriteLine("Выберите цвет змейки:");
            Console.WriteLine("1 - Зеленый\n2 - Красный\n3 - Синий");
            ConsoleKey colorKey = Console.ReadKey(true).Key;
            if (colorKey == ConsoleKey.D1) snakeColor = ConsoleColor.Green;
            else if (colorKey == ConsoleKey.D2) snakeColor = ConsoleColor.Red;
            else if (colorKey == ConsoleKey.D3) snakeColor = ConsoleColor.Blue;

            Console.Clear();
            Console.WriteLine("Выберите цвет фона:");
            Console.WriteLine("1 - Черный\n2 - Серый\n3 - Синий");
            ConsoleKey bgKey = Console.ReadKey(true).Key;
            if (bgKey == ConsoleKey.D2) backgroundColor = ConsoleColor.Gray;
            else if (bgKey == ConsoleKey.D3) backgroundColor = ConsoleColor.DarkBlue;
            else backgroundColor = ConsoleColor.Black;

            Console.BackgroundColor = backgroundColor;
            Console.Clear();
        }

        public void RunGame()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = snakeColor;
            Console.BackgroundColor = backgroundColor;

            int x = 10;
            int y = 10;
            int dx = 1;
            int dy = 0;

            DateTime lastMove = DateTime.Now;

            while (true)
            {
                PrintScore();

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow: dx = 0; dy = -1; break;
                        case ConsoleKey.DownArrow: dx = 0; dy = 1; break;
                        case ConsoleKey.LeftArrow: dx = -1; dy = 0; break;
                        case ConsoleKey.RightArrow: dx = 1; dy = 0; break;
                        case ConsoleKey.Escape: return;
                    }
                }

                if ((DateTime.Now - lastMove).TotalMilliseconds >= (int)speed)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");

                    x += dx;
                    y += dy;

                    // Если змейка ушла за экран — конец
                    if (x < 0 || x >= Console.WindowWidth || y < 1 || y >= Console.WindowHeight)
                        break;

                    Console.SetCursorPosition(x, y);
                    Console.Write("■");

                    lastMove = DateTime.Now;
                }

                Thread.Sleep(1);
            }

            Console.Clear();
            Console.WriteLine($"Игра окончена! Очки: {score}");
        }

        public bool AskRestart()
        {
            Console.WriteLine("\nНажмите Y для новой игры или любую другую клавишу для выхода.");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }

        public void PrintScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Очки: {score}");
            Console.ForegroundColor = snakeColor;
        }
    }
}
