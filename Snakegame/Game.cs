using System;
using System.Diagnostics;
using System.Threading;

namespace Snakegame
{
    public class Game
    {
        private Snake snake;
        private Food food;
        private FoodCreator foodCreator;
        private Walls walls;
        private Stopwatch stopwatch;
        private int score;
        private int gameSpeed;
        private string playerName;

        public void Start()
        {
            ConsoleUI.SetupWindow(80, 25);
            ConsoleUI.ShowStartMessage();
            ConsoleUI.WaitForKeyPress(ConsoleKey.Spacebar);
            ConsoleUI.ShowGameRules();
            ConsoleUI.WaitForKeyPress(ConsoleKey.Spacebar);

            playerName = ConsoleUI.GetPlayerName();
            gameSpeed = ConsoleUI.ChooseDifficulty();

            StartNewGame();
        }

        private void StartNewGame()
        {
            snake = new Snake(new Point(10, 10, '@'), 5, Direction.Right);
            foodCreator = new FoodCreator(78, 23);
            food = foodCreator.CreateFood();
            walls = new Walls(80, 25);
            stopwatch = new Stopwatch();
            score = 0;

            Console.Clear();
            walls.Draw();
            snake.Draw();
            food.Draw();
            Sounds.PlayMusic();

            stopwatch.Start();
            while (true)
            {
                if (ConsoleUI.CheckGameOver(snake, walls))
                    break;

                ConsoleUI.DisplayGameStats(score, stopwatch.Elapsed);
                ConsoleUI.HandleKeyPress(snake);

                if (snake.Eat(food))
                {
                    score++;
                    Sounds.PlayEat();
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(gameSpeed);
            }

            GameOver();
        }

        private void GameOver()
        {
            Sounds.StopMusic();
            Sounds.PlayGameOver();

            stopwatch.Stop();
            ConsoleUI.EndGame(playerName, score, stopwatch);

            Leaderboard.SaveResult(playerName, score);

            if (ConsoleUI.AskToPlayAgain())
                StartNewGame();
            else
                Environment.Exit(0);
        }
    }
}
