using System;

namespace KCK_Project_MVVM_Console
{
    public static class Diagram
    {
        public static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('#', 50));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(CenterText(header));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('#', 50));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void DisplayFooter()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('#', 50));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nNaciśnij ESC, aby wrócić do menu.");
        }

        public static string CenterText(string text, int totalWidth = 50)
        {
            int padding = (totalWidth - text.Length) / 2;
            return text.PadLeft(text.Length + padding).PadRight(totalWidth);
        }
    }
}
