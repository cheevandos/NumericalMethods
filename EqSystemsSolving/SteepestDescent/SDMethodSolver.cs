using System;
using System.Threading;

namespace SteepestDescent
{
    public class SDMethodSolver
    {
        private const decimal ALPHA = 0.085M;
        private int currentIteration;
        private decimal currentXFirst;
        private decimal currentXSecond;
        private decimal currentDiff;
        private readonly decimal epsilon;
        private readonly int decimals;
        private readonly IPrinter printer;

        public SDMethodSolver(decimal xFirstStart, decimal xSecondStart, 
            decimal epsilon, IPrinter printer)
        {
            currentXFirst = xFirstStart;
            currentXSecond = xSecondStart;
            currentIteration = 0;
            this.epsilon = epsilon;
            decimals = epsilon.ToString().Split(',')[1].Length;
            this.printer = printer;
        }

        private bool AccuracyAchieved()
        {
            return currentDiff <= epsilon;
        }

        private void CalculateDiff(decimal xFirst, decimal xSecond)
        {
            currentDiff = Math.Round(Math.Max(Math.Abs(currentXFirst - xFirst),
                Math.Abs(currentXSecond - xSecond)), decimals);
        }

        private decimal FirstDerivate(decimal xFirst, decimal xSecond)
        {
            return ALPHA * (4 * xSecond * Convert.ToDecimal(Math.Cos(Convert.ToDouble(xFirst)))
                - 4 * Convert.ToDecimal(Math.Cos(Convert.ToDouble(xFirst))) 
                - 2 * xSecond * Convert.ToDecimal(Math.Sin(Convert.ToDouble(xFirst))) 
                + 3 * Convert.ToDecimal(Math.Sin(Convert.ToDouble(xFirst))));
        }

        private decimal SecondDerivate(decimal xFirst, decimal xSecond)
        {
            return ALPHA * (4 * Convert.ToDecimal(Math.Sin(Convert.ToDouble(xFirst))) 
                + 10 * xSecond - 11 + 2 * Convert.ToDecimal(Math.Cos(Convert.ToDouble(xFirst))));
        }

        private void Print()
        {
            string data = $"Итерация #{currentIteration}\t" +
                          $"X1 = {Math.Round(currentXFirst, decimals)} " +
                          $"X2 = {Math.Round(currentXSecond, decimals)}" +
                          $" Разность = {Math.Round(currentDiff, decimals)}";
            Thread.Sleep(Convert.ToInt32(1000 / (decimals * decimals)));
            printer.Print(data);
        }

        private void PrintInitialData()
        {
            string initialData = $"\nМетод наискорейшего спуска\n" +
                                 $"\nНачальное приближение: " +
                                 $"X1 = {Math.Round(currentXFirst, decimals)} " +
                                 $"X2 = {Math.Round(currentXSecond, decimals)}\n" +
                                 $"\nТочность: {epsilon}\n" +
                                 $"\nШаговый множитель: {ALPHA}\n";
            printer.Print(initialData);
        }


        public void Process()
        {
            PrintInitialData();
            decimal tempXFirst;
            decimal tempXSecond;
            do
            {
                tempXFirst = currentXFirst;
                tempXSecond = currentXSecond;
                currentXFirst = tempXFirst - FirstDerivate(tempXFirst, tempXSecond);
                currentXSecond = tempXSecond - SecondDerivate(tempXFirst, tempXSecond);
                CalculateDiff(tempXFirst, tempXSecond);
                currentIteration++;
                Print();
            } while (!AccuracyAchieved());
            string finalData = $"\n{Math.Round(currentDiff, decimals)} " +
                               $"<= {Math.Round(epsilon, decimals)} " +
                               $"- условие выполняется\n" +
                               $"\nВычисление закончено на итерации #{currentIteration}\n" +
                               $"\nРешение системы: " +
                               $"X1 = {Math.Round(currentXFirst, decimals)}" +
                               $" X2 = {Math.Round(currentXSecond, decimals)}";
            printer.Print(finalData);
        }
    }
}
