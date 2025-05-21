using System;
using System.Threading;
using GameSnake;

namespace GameSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(40, 20);
            game.Run();
        }
    }
}
