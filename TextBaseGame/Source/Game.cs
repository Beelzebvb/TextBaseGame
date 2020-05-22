using System;
using TextBaseGame.Manager;

namespace TextBaseGame
{
    class Game
    {
        static void Main(string[] args)
        {

            GameManager.Instance.Run();

            Console.WriteLine("Bye");
        }
    }
}
