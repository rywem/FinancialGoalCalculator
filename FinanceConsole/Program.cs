// See https://aka.ms/new-console-template for more information
using FinanceConsole;
using FinancialGoalCalculator.Web.Helpers;

Console.WriteLine("Hello, World!");

//Console.WriteLine(FinanceHelper.MonthlyPayment(356250, 2.875m, 360));
//FinanceHelper.CompoundCalculator(2.875, 360, 356250, new DateTime(2021, 10, 1));
//FinanceHelper.CompoundCalculator(7, 36, 1000);

TimeValueCalculations calcs = new TimeValueCalculations();
calcs.Run();