using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceConsole
{
    public class TimeValueCalculations
    {
        public void Run()
        {
            decimal pv = 15000;
            decimal fv = 0;
            decimal i = 5.25m;
            int n = 12;
            int t = 10;

            Console.WriteLine(CalculateFutureValue(pv, i, n, t).ToString("c"));
        }

        public decimal CalculateFutureValue(decimal pv, decimal interestRate, int nPeriods, int tYears) 
        {
            double pvDouble = (double)pv;
            double interestRateDouble = (double)interestRate;
            double nPeriodsDouble = (double)nPeriods;
            double tYearsDouble = (double)tYears;

            double futureValueDouble = pvDouble * Math.Pow((1 + (interestRateDouble / nPeriodsDouble / 100)), (nPeriodsDouble * tYearsDouble));
            return (decimal)futureValueDouble;
        }

        /// <summary>
        /// Calculates the future value of a specific 
        /// </summary>
        /// <param name="pv">Present Value</param>
        /// <param name="monthlyInterestRate">Divide by Period and 100 first</param>
        /// <param name="specificPeriod">The specific period</param>
        /// <returns></returns>
        public decimal CalculateFutureValueSpecificPeriod(decimal pv, decimal monthlyInterestRate, int specificPeriod)
        {
            double pvDouble = (double)pv;
            double interestRateDouble = (double)monthlyInterestRate;
            double nPeriodDouble = (double)specificPeriod;
            double futureValueDouble = pvDouble * Math.Pow((1 + interestRateDouble), nPeriodDouble);
            return (decimal)futureValueDouble;
        }

    }
}
