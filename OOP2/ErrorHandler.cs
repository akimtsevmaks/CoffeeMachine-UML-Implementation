using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeMachine
{
    internal class ErrorHandler
    {
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n {message}");
            Console.ResetColor();
            Console.WriteLine("Нажмите любую кнопку...");
            Console.ReadKey();
        }
    }
}
