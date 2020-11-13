using System;

namespace SteepestDescent
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMethod();
        }

        public static void TestMethod()
        {
            decimal xFirst = 1;
            decimal xSecond = 1;
            Console.Write("Введите точность ===> ");
            decimal epsilon = Convert.ToDecimal(Console.ReadLine());
            ConsolePrinter printer = new ConsolePrinter();
            SDMethodSolver solver = new SDMethodSolver(xFirst, xSecond, epsilon, printer);
            solver.Process();
            Console.ReadKey();
        }
    }
}
