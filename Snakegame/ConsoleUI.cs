// Console.cs
// Класс отвечает за интерфейс пользователя: отображение текста, ввод имени, завершение игры


using System;
using System.Diagnostics;

namespace Snakegame
{
    public static class ConsoleUI
    {
        public static void SetupWindow(int width, int height)
        {
            System.Console.SetWindowSize(width, height);
            System.Console.BufferHeight = height;
            System.Console.BufferWidth = width;
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Clear();
        }

        public static void ShowStartMessage()
        {
            string message = "You play as Hungry At sign -> @";
            string prompt = "Press SPACE to continue...";
            DisplayCenteredMessage(message, -2);
            DisplayCenteredMessage(prompt, 2);
        }

        public static void ShowGameRules()
        {
            System.Console.Clear();
            string rules = "Eat $ to gain 1 point. Avoid hitting yourself or the wall.";
            string prompt = "Press SPACE to start the game...";
            DisplayCenteredMessage(rules, 0);
            DisplayCenteredMessage(prompt, 2);
        }

        public static void DisplayCenteredMessage(string message, int yOffset)
        {
            int x = System.Console.WindowWidth / 2 - message.Length / 2;
            int y = System.Console.WindowHeight / 2 + yOffset;
            System.Console.SetCursorPosition(x, y);
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(message);
        }

        public static string GetPlayerName()
        {
            System.Console.Clear();
            string prompt = "Write your name (min 3 letters): ";
            int x = System.Console.WindowWidth / 2 - prompt.Length / 2;
            int y = System.Console.WindowHeight / 2 - 2;
            System.Console.SetCursorPosition(x, y);
            System.Console.Write(prompt);

            string name = "";
            while (name.Length < 3)
            {
                try
                {
                    name = System.Console.ReadLine();
                    if (name.Length < 3)
                        throw new Exception("Name too short");
                }
                catch
                {
                    System.Console.WriteLine("Name must be at least 3 characters.");
                }
            }
            return name;
        }

        public static void WaitForKeyPress(ConsoleKey key)
        {
            while (System.Console.ReadKey(true).Key != key) { }
        }

        public static void DisplayGameStats(int score, TimeSpan timeElapsed)
        {
            System.Console.SetCursorPosition(0, 0);
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write($"Score: {score} | Time: {timeElapsed.Minutes:D2}:{timeElapsed.Seconds:D2}");
        }

        public static void HandleKeyPress(Snake snake)
        {
            if (System.Console.KeyAvailable)
            {
                ConsoleKey key = System.Console.ReadKey(true).Key;
                snake.HandleKey(key);
            }
        }

        public static bool CheckGameOver(Snake snake, Walls walls)
        {
            if (walls.IsHit(snake) || snake.IsHitTail())
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.Red;
                DisplayCenteredMessage("GAME OVER", 0);
                return true;
            }
            return false;
        }

        public static void EndGame(string playerName, int score, Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Leaderboard.SaveScore(playerName, score, stopwatch.Elapsed);
            System.Console.SetCursorPosition(0, System.Console.WindowHeight - 1);
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}

