using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSnake;
using System.Media;
using NAudio.Wave;
using System.IO;


namespace GameSnake
{
    internal class Game
    {
        private Walls walls;
        private Snake snake;
        private FoodCreator goodFoodCreator;
        private FoodCreator badFoodCreator;
        private Point goodFood;
        private List<Point> badFoods;

        private int score;
        private int mapWidth;
        private int mapHeight;
        private int speed;
        private int badFoodCount = 10;

        private string playerName;

        // Звук
        private SoundPlayer goodSound;
        private SoundPlayer badSound;

        private IWavePlayer backgroundOutput;
        private AudioFileReader backgroundReader;

        public Game(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;
        }

        public void Run()
        {
            while (true)
            {
                ShowStartScreen();
                StartNewGame();
                RunGameLoop();

                if (!AskRestart())
                    break;
            }
        }

        private void StartNewGame()
        {
            score = 0;

            // Инициализация звуков еды
            goodSound = new SoundPlayer("Sounds/good.wav");
            badSound = new SoundPlayer("Sounds/bad.wav");

            // Остановка и очистка предыдущей фоновой музыки, если была
            backgroundOutput?.Stop();
            backgroundOutput?.Dispose();
            backgroundReader?.Dispose();

            // Запуск фоновой музыки через NAudio
            backgroundReader = new AudioFileReader("Sounds/background.wav");
            backgroundReader.Volume = 0.2f; // Громкость фона
            backgroundOutput = new WaveOutEvent();
            backgroundOutput.Init(backgroundReader);
            backgroundOutput.Play();

            Console.Clear();
            walls = new Walls(mapWidth, mapHeight);

            Point tail = new Point(mapWidth / 2 - 4, mapHeight / 2, 'O', ConsoleColor.Green);
            snake = new Snake(tail, 4, ConsoleColor.Green);

            goodFoodCreator = new FoodCreator(mapWidth - 2, mapHeight - 2, '*', ConsoleColor.Red);
            badFoodCreator = new FoodCreator(mapWidth - 2, mapHeight - 2, 'X', ConsoleColor.DarkYellow);

            goodFood = goodFoodCreator.CreateFood(snake);

            badFoods = new List<Point>();
            for (int i = 0; i < badFoodCount; i++)
            {
                Point bad = badFoodCreator.CreateFood(snake);
                badFoods.Add(bad);
            }
        }

        private void RunGameLoop()
        {
            Console.Clear();
            Console.CursorVisible = false;
            walls.Draw();
            snake.Draw();
            goodFood.Draw();
            foreach (var bad in badFoods)
                bad.Draw();

            DrawScore();

            bool started = false;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    snake.SetDirection(key);
                    if (key == ConsoleKey.Escape) break;
                    started = true;
                }

                if (!started)
                {
                    Thread.Sleep(100);
                    continue;
                }

                Point nextHead = snake.GetNextPoint();

                if (walls.IsHit(nextHead) || snake.IsHitTail())
                    break;

                if (snake.Eat(goodFood))
                {
                    score += 10;
                    goodSound.Play();

                    goodFood.Clear();
                    goodFood = goodFoodCreator.CreateFood(snake);
                    goodFood.Draw();
                    snake.Draw();
                    if (speed > 50) speed -= 5;
                }
                else
                {
                    bool ateBad = false;
                    for (int i = 0; i < badFoods.Count; i++)
                    {
                        if (snake.Eat(badFoods[i]))
                        {
                            snake.Reduce();
                            score -= 5;
                            badSound.Play();

                            if (score < 0)
                            {
                                
                                break;
                            }

                            badFoods[i].Clear();
                            badFoods[i] = badFoodCreator.CreateFood(snake);
                            badFoods[i].Draw();
                            snake.Draw();
                            ateBad = true;
                            break;
                        }
                    }

                    if (!ateBad)
                        snake.Move();
                }

                if (score < 0)
                    break;

                DrawScore();
                Thread.Sleep(speed);
            }

            ShowGameOverScreen();
            SaveScore();
        }

        private void ShowStartScreen()
        {
            Console.Clear();
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Добро пожаловать в игру Змейка! ===");
            Console.ResetColor();

            Console.WriteLine("Правила игры:");
            Console.WriteLine("- Управление стрелками");
            Console.WriteLine("- Собирайте красную еду (*) — она увеличивает длину змейки и даёт очки.");
            Console.WriteLine("- Избегайте плохой еды (X) — отнимает очки.");
            Console.WriteLine("- Если счёт опускается ниже нуля — Игра окончена.");
            Console.WriteLine("- Нельзя врезаться в стены или в саму себя.");
            Console.WriteLine("- Игра начинается после нажатия стрелки.\n");
            Console.WriteLine("----------------------------------------");

            Console.Write("\nВведите ваше имя: ");
            playerName = Console.ReadLine();

            Console.WriteLine("\nВыберите уровень сложности:");
            Console.WriteLine("1 - Легкий");
            Console.WriteLine("2 - Средний");
            Console.WriteLine("3 - Сложный");

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        speed = 200;
                        badFoodCount = 5;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        speed = 100;
                        badFoodCount = 10;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        speed = 50;
                        badFoodCount = 15;
                        break;
                }
            } while (key != ConsoleKey.D1 && key != ConsoleKey.NumPad1 &&
                     key != ConsoleKey.D2 && key != ConsoleKey.NumPad2 &&
                     key != ConsoleKey.D3 && key != ConsoleKey.NumPad3);

            Console.Clear();
            Console.CursorVisible = false;
        }

        private void ShowGameOverScreen()
        {
            backgroundOutput?.Stop();

            Console.Clear();
            Console.SetCursorPosition(mapWidth / 3, mapHeight / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Игра окончена!");
            Console.ResetColor();
            Console.SetCursorPosition(mapWidth / 3, mapHeight / 2 + 1);
            Console.WriteLine($"Игрок: {playerName}");
            Console.SetCursorPosition(mapWidth / 3, mapHeight / 2 + 2);
            Console.WriteLine($"Ваш счёт: {score}");
            Console.SetCursorPosition(mapWidth / 3, mapHeight / 2 + 4);
            Console.Write("Нажмите Enter, чтобы сыграть снова или Escape для выхода...");
        }

        private bool AskRestart()
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                    return true;
                else if (key == ConsoleKey.Escape)
                    return false;
            }
        }

        private void DrawScore()
        {
            if (mapHeight < Console.BufferHeight)
            {
                Console.SetCursorPosition(0, mapHeight);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Счёт: {score} ");
                Console.ResetColor();
            }
        }
        private void SaveScore()
        {
            string filePath = "scores.txt";
            string line = $"{playerName} - {score}";

            try
            {
                File.AppendAllText(filePath, line + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, mapHeight + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка записи результата: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}

