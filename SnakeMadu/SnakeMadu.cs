using System;

namespace SnakeGame1
{
    enum SpeedLevel { Slow = 150, Normal = 100, Fast = 50 }

    class SnakeMadu
    {
        private int score;
        private SpeedLevel speed;
        private DateTime startTime;
        private ConsoleColor snakeColor;
        private ConsoleColor backgroundColor;

        public SnakeMadu()
        {
            score = 0;
            speed = SpeedLevel.Normal;
            startTime = DateTime.Now;
            snakeColor = ConsoleColor.Green;
            backgroundColor = ConsoleColor.Black;
        }

        public void ShowStartupOptions()
        {
            Console.Clear();
            Console.WriteLine("Выберите скорость игры:");
            Console.WriteLine("1 — Медленно\n2 — Нормально\n3 — Быстро");
            ConsoleKey speedKey = Console.ReadKey(true).Key;
            if (speedKey == ConsoleKey.D1) speed = SpeedLevel.Slow;
            else if (speedKey == ConsoleKey.D2) speed = SpeedLevel.Normal;
            else if (speedKey == ConsoleKey.D3) speed = SpeedLevel.Fast;

            Console.Clear();
            Console.WriteLine("Выберите цвет змейки:");
            Console.WriteLine("1 — Зеленый\n2 — Красный\n3 — Синий");
            ConsoleKey colorKey = Console.ReadKey(true).Key;
            if (colorKey == ConsoleKey.D1) snakeColor = ConsoleColor.Green;
            else if (colorKey == ConsoleKey.D2) snakeColor = ConsoleColor.Red;
            else if (colorKey == ConsoleKey.D3) snakeColor = ConsoleColor.Blue;

            Console.Clear();
            Console.WriteLine("Выберите цвет фона:");
            Console.WriteLine("1 — Черный\n2 — Серый\n3 — Синий");
            ConsoleKey bgKey = Console.ReadKey(true).Key;
            if (bgKey == ConsoleKey.D1) backgroundColor = ConsoleColor.Black;
            else if (bgKey == ConsoleKey.D2) backgroundColor = ConsoleColor.Gray;
            else if (bgKey == ConsoleKey.D3) backgroundColor = ConsoleColor.DarkBlue;

            Console.Clear();
            Console.BackgroundColor = backgroundColor;
            Console.Clear();
        }

        public TimeSpan GetElapsedTime() => DateTime.Now - startTime;

        public void EatFood()
        {
            score++;
            if (score % 5 == 0 && (int)speed > 50)
                speed = (SpeedLevel)((int)speed - 10);
        }

        public void EatBonusFood()
        {
            score += 3;
            if ((int)speed > 50)
                speed = (SpeedLevel)((int)speed - 15);
        }

        public int GetScore() => score;
        public int GetSpeedDelay() => (int)speed;
        public ConsoleColor GetSnakeColor() => snakeColor;
        public ConsoleColor GetBackgroundColor() => backgroundColor;

        public void PrintScore()
        {
            int originalCursorLeft = Console.CursorLeft;
            int originalCursorTop = Console.CursorTop;

            Console.SetCursorPosition(Console.WindowWidth - 20, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Очки: {score}");
            Console.ResetColor();

            Console.SetCursorPosition(Console.WindowWidth - 40, 0);
            TimeSpan elapsedTime = GetElapsedTime();
            TimeSpan roundedTime = TimeSpan.FromSeconds(Math.Round(elapsedTime.TotalSeconds));
            Console.Write($"Время: {roundedTime:hh\\:mm\\:ss}");

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Еда: $ (+1)  |  Бонус: X (+3)");
            Console.ResetColor();

            Console.SetCursorPosition(originalCursorLeft, originalCursorTop);
        }

        public bool AskRestart()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);
            Console.Write("Игра окончена. Начать заново? (Y/N)");
            ConsoleKey key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Y;
        }
    }
}
