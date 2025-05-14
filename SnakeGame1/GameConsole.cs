using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame1
{
    class GameConsole
    {
        public static void SetupWindow(int width, int height)
        {
            System.Console.SetWindowSize(width, height);
            System.Console.Clear();
        }

        public static void ShowStartMessage()
        {
            string message = "Вы играете за голодный символ @";
            string prompt = "Нажмите ПРОБЕЛ, чтобы продолжить...";
            DisplayCenteredMessage(message, -2);
            DisplayCenteredMessage(prompt, 2);
        }

        public static void DisplayCenteredMessage(string message, int yOffset)
        {
            int x = System.Console.WindowWidth / 2 - message.Length / 2;
            int y = System.Console.WindowHeight / 2 + yOffset;
            System.Console.SetCursorPosition(x, y);
            System.Console.WriteLine(message);
        }

        public static string GetPlayerName()
        {
            System.Console.Clear();
            string prompt = "Введите ваше имя: ";
            int x = System.Console.WindowWidth / 2 - prompt.Length / 2;
            int y = System.Console.WindowHeight / 2 - 2;
            System.Console.SetCursorPosition(x, y);
            System.Console.Write(prompt);
            return System.Console.ReadLine();
        }

        public static void WaitForKeyPress(ConsoleKey key)
        {
            while (System.Console.ReadKey(true).Key != key) { }
        }

        public static void ShowGameRules()
        {
            System.Console.Clear();
            string rules = "Вы играете за символ @. Ешьте $ — +1 очко. Ешьте X — -1 очко.";
            string prompt = "Нажмите ПРОБЕЛ, чтобы начать игру...";
            DisplayCenteredMessage(rules, 0);
            DisplayCenteredMessage(prompt, 2);
        }

        public static bool CheckGameOver(Snake snake, Walls walls)
        {
            if (walls.IsHit(snake) || snake.IsHitTail())
            {
                System.Console.Clear();
                string gameOverMessage = "ИГРА ОКОНЧЕНА";
                DisplayCenteredMessage(gameOverMessage, 0);
                return true;
            }
            return false;
        }

        public static void DisplayGameStats(int foodCounter, TimeSpan elapsedTime)
        {
            System.Console.SetCursorPosition(0, 0);
            System.Console.Write($"Очки: {foodCounter}   Время: {elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}");
        }

        public static void HandleKeyPress(Snake snake)
        {
            if (System.Console.KeyAvailable)
            {
                ConsoleKey key = System.Console.ReadKey(true).Key;
                snake.HandleKey(key);
            }
        }

        public static void ShowGameOver(string playerName, int foodCounter, TimeSpan timeElapsed)
        {
            System.Console.Clear();
            string message = "Игра окончена";
            string stats = $"Игрок: {playerName} | Очки: {foodCounter} | Время: {timeElapsed.Minutes:D2}:{timeElapsed.Seconds:D2}";
            DisplayCenteredMessage(message, -1);
            DisplayCenteredMessage(stats, 1);

            // Сохраняем результат
            Leaderboard.SaveScore(playerName, foodCounter, timeElapsed);
        }
    }
}
