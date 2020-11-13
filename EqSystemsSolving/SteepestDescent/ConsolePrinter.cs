using System;

namespace SteepestDescent
{
    class ConsolePrinter : IPrinter
    {
        public void Print(string data)
        {
            Console.WriteLine(data);
        }
    }
}
