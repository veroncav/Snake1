using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame1
{
    class Snake
    {
        public Queue<Point> body; // Хранит координаты тела змейки
        public Direction direction; // Текущее направление движения змейки
        private SnakeMadu game; // Ссылка на игру, чтобы получить настройки (например, цвет змейки)

        // Конструктор змейки
        public Snake(Point tail, int length, Direction direction, SnakeMadu game)
        {
            body = new Queue<Point>();
            this.direction = direction;
            this.game = game;

            // Генерация тела змейки от хвоста в сторону головы
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction); // Двигаем каждую следующую часть тела
                body.Enqueue(p);
            }
        }

        // Движение змейки
        public void Move()
        {
            Point tail = body.Dequeue(); // Удаляем хвост
            Point head = GetNextPoint(); // Получаем следующую точку головы
            body.Enqueue(head); // Добавляем её в тело змейки

            tail.Clear(); // Очищаем символ с консоли

            Console.ForegroundColor = game.GetSnakeColor(); // Устанавливаем цвет змейки
            head.Draw(); // Отрисовываем голову
            Console.ResetColor(); // Сброс цвета
        }

        // Получение следующей координаты в текущем направлении
        public Point GetNextPoint()
        {
            Point head = body.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        // Проверка, столкнулась ли змейка с чем-то
        public bool IsHit(Point p)
        {
            return body.Any(b => b.IsHit(p));
        }

        // Увеличение змейки при съедании еды
        public void Grow()
        {
            Point head = GetNextPoint();
            body.Enqueue(head);

            Console.ForegroundColor = game.GetSnakeColor();
            head.Draw();
            Console.ResetColor();
        }
    }
}