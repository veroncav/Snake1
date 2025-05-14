using System;
using System.Diagnostics;

namespace SnakeGame1
{
    public static class GameConsole
    {
        public static void SetupWindow(int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.Clear();
        }

        public static void ShowStartMessage()
        {
            string message = "Вы играете за голодного символа @";
            string prompt = "Нажмите ПРОБЕЛ для продолжения...";
            DisplayCenteredMessage(message, -2);
            DisplayCenteredMessage(prompt, 2);
        }

        public static void ShowGameRules()
        {
            Console.Clear();
            string rules = "Вы играете за @. Ешьте $ для +1 очка. Ешьте X — теряете 1 очко.";
            string prompt = "Нажмите ПРОБЕЛ, чтобы начать игру...";
            DisplayCenteredMessage(rules, 0);
            DisplayCenteredMessage(prompt, 2);
        }

        public static void DisplayCenteredMessage(string message, int yOffset)
        {
            int x = Console.WindowWidth / 2 - message.Length / 2;
            int y = Console.WindowHeight / 2 + yOffset;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(message);
        }

        public static string GetPlayerName()
        {
            Console.Clear();
            string prompt = "Введите ваше имя: ";
            int x = Console.WindowWidth / 2 - prompt.Length / 2;
            int y = Console.WindowHeight / 2 - 2;
            Console.SetCursorPosition(x, y);
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static void WaitForKeyPress(ConsoleKey key)
        {
            while (Console.ReadKey(true).Key != key) { }
        }

        public static bool CheckGameOver(Snake snake, Walls walls)
        {
            if (walls.IsHit(snake) || snake.IsHitTail())
            {
                Console.Clear();
                string gameOverMessage = "ИГРА ОКОНЧЕНА";
                DisplayCenteredMessage(gameOverMessage, 0);
                return true;
            }
            return false;
        }

        public static void DisplayGameStats(int foodCounter, TimeSpan elapsedTime)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Съедено: {foodCounter}  Время: {elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}");
        }

        public static void HandleKeyPress(Snake snake)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                snake.HandleKey(key);
            }
        }

        public static void EndGame(string playerName, int foodCounter, Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Leaderboard.SaveScore(playerName, foodCounter, stopwatch.Elapsed);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        public static bool AskToRestart()
        {
            Console.WriteLine("Хотите сыграть ещё раз? (Y/N)");
            ConsoleKey key = Console.ReadKey(true).Key;
            return key == ConsoleKey.Y;
        }
    }
}
