using System;
using System.Collections.Generic;
using System.Linq;

namespace Snakegame
{
    public class Snake
    {
        private List<Point> body;
        private Direction direction;
        private char bodySymbol = 'o';
        private char headSymbol = '@';

        public Snake(Point tail, int length, Direction dir)
        {
            direction = dir;
            body = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                var p = new Point(tail.X + i, tail.Y, bodySymbol);
                body.Add(p);
            }
            // Помечаем голову отдельно
            body.Last().Symbol = headSymbol;
        }

        public void Move()
        {
            // Очистка хвоста
            Point tail = body.First();
            tail.Clear();
            body.RemoveAt(0);

            // Добавление новой головы
            Point head = GetNextPoint();
            head.Symbol = headSymbol;

            // Предыдущая голова становится телом
            body.Last().Symbol = bodySymbol;

            body.Add(head);
            head.Draw();
        }

        public Point GetNextPoint()
        {
            Point head = body.Last();
            Point next = head.Clone();
            switch (direction)
            {
                case Direction.Left: next.X--; break;
                case Direction.Right: next.X++; break;
                case Direction.Up: next.Y--; break;
                case Direction.Down: next.Y++; break;
            }
            return next;
        }

        public void HandleKey(ConsoleKey key)
        {
            Direction newDir = direction;

            switch (key)
            {
                case ConsoleKey.LeftArrow: newDir = Direction.Left; break;
                case ConsoleKey.RightArrow: newDir = Direction.Right; break;
                case ConsoleKey.UpArrow: newDir = Direction.Up; break;
                case ConsoleKey.DownArrow: newDir = Direction.Down; break;
            }

            // Запрет на поворот в обратную сторону
            if (!IsOppositeDirection(newDir))
                direction = newDir;
        }

        private bool IsOppositeDirection(Direction newDir)
        {
            return (direction == Direction.Left && newDir == Direction.Right) ||
                   (direction == Direction.Right && newDir == Direction.Left) ||
                   (direction == Direction.Up && newDir == Direction.Down) ||
                   (direction == Direction.Down && newDir == Direction.Up);
        }

        public bool Eat(Point food)
        {
            Point next = GetNextPoint();
            if (next.IsHit(food))
            {
                // Новая голова
                next.Symbol = headSymbol;

                // Старая голова — тело
                body.Last().Symbol = bodySymbol;

                body.Add(next);
                next.Draw();
                return true;
            }
            return false;
        }

        public bool IsHitTail()
        {
            Point head = body.Last();
            return body.Take(body.Count - 1).Any(p => p.IsHit(head));
        }

        public void Draw()
        {
            foreach (var p in body)
                p.Draw();
        }

        public List<Point> GetBody() => body;

        public Point GetHead() => body.Last();
    }
}
