using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSnake
{
    internal class Snake : Figure
    {
        private int dx;
        private int dy;
        private ConsoleColor color; // Добавляем поле цвета змейки

        public Snake(Point tail, int length, ConsoleColor color)
        {
            dx = 1; // движение вправо
            dy = 0;
            pList = new List<Point>();

            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail.X + i, tail.Y, 'O', color); // ← вот тут изменение
                pList.Add(p);
            }
        }


        public void Move()
        {
            Point tail = pList[0];
            pList.RemoveAt(0); // удаляем хвост
            tail.Clear();      // стираем хвост

            Point head = GetNextPoint();
            pList.Add(head);   // добавляем голову
            head.Draw();       // рисуем голову
        }

        public Point GetNextPoint()
        {
            Point head = pList[pList.Count - 1];
            return new Point(head.X + dx, head.Y + dy, head.Symbol, head.Color);
        }

        public void SetDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (dx == 0) { dx = -1; dy = 0; }
                    break;
                case ConsoleKey.RightArrow:
                    if (dx == 0) { dx = 1; dy = 0; }
                    break;
                case ConsoleKey.UpArrow:
                    if (dy == 0) { dx = 0; dy = -1; }
                    break;
                case ConsoleKey.DownArrow:
                    if (dy == 0) { dx = 0; dy = 1; }
                    break;
            }
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();

            if (head.IsHit(food))
            {
                Point newHead = new Point(food.X, food.Y, 'O', pList[0].Color);
                pList.Add(newHead);
                newHead.Draw(); // ← обязательно рисуем!
                return true;
            }
            return false;
        }

        public void Reduce()
        {
            if (pList.Count > 1)
            {
                Point tail = pList[0];
                tail.Clear();
                pList.RemoveAt(0);
            }
        }

        public bool IsHitTail()
        {
            Point head = GetNextPoint(); // ← поправка

            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }

        public bool IsHit(Point point)
        {
            foreach (var p in pList)
            {
                if (p.IsHit(point))
                    return true;
            }
            return false;
        }
    }
}