using System;

namespace TextBaseGame.Utilities
{
    static class ConsoleUI
    {
        public static void AddHeader(string content, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"==={content}===");
            Console.ResetColor();
        }

        public static void DrawLine(int count = 30, string pattern = "-", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < count; i++)
            {
                Console.Write(pattern);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void AddOption(string content, string option = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            if (option.Length <= 1)
                Console.WriteLine($" {option} : {content}");
            else
                Console.WriteLine($"{option} : {content}");
            Console.ResetColor();
        }

        public static void AddList(string[] content, int startIndex = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            int index = startIndex;
            for (int i = 0; i < content.Length; i++)
            {
                string line = (string)content[i];
                Console.WriteLine($"{index} : {line}");
                index++;
            }
            Console.ResetColor();
        }

        public static void Warning(string message, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void Error(string message, ConsoleColor color = ConsoleColor.DarkRed)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
