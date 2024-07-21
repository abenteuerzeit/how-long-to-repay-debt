using System.Globalization;
using How_Long_To_Pay_It_Off;
using Microsoft.Extensions.Configuration;

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build() ?? throw new ArgumentNullException(nameof(configurationBuilder));

var settings = configuration.GetSection("LoanSettings").Get<LoanSettings>();
if (settings is null) throw new ArgumentNullException(nameof(settings));

var initialLoan = settings.InitialLoan;
var payment = settings.Payment;
var yearsPaid = settings.YearsPaid;
var remainingLoan = settings.RemainingLoan;
var monthsPaid = yearsPaid * 12;
var culture = CultureInfo.CurrentCulture;


var seperator = new string('-', Console.WindowWidth - 12);

var scenario =
    $"""
     
        Project Inspired by AndyMath.com
        https://vm.tiktok.com/ZGenudJQC/
        Code by Adrian Mróz
        {seperator}
        
        
        My wife and I left graduate school {yearsPaid} years ago with a combined total of {initialLoan.ToString("C", culture)} debt.
        Since then we've made {payment.ToString("C", culture)} monthly payments for {yearsPaid} years ({(payment * monthsPaid).ToString("C", culture)}).
        Today, we still owe {remainingLoan.ToString("C", culture)}.
     
         1. What is the annual interest rate that would cause this? 
         2. How long is it going to take them to pay all the debt off? 
     """;


Console.WriteLine(scenario);

var calculator = new Calculator(initialLoan, payment, monthsPaid, remainingLoan, culture);
var monthlyRate = Calculator.GetMonthlyInterestRate(initialLoan, payment, monthsPaid, remainingLoan);
var annualRate = calculator.GetEffectiveAnnualInterestRate();

Console.WriteLine($"\tMonthly Interest Rate: {monthlyRate.ToString("F12", culture)}");
Console.WriteLine($"\tAnnual Interest Rate: {annualRate.ToString("P2", culture)}");
Console.WriteLine();

var howLongToPayOff = calculator.GetPayTable();
var indentedOutput = Utility.IndentTable(howLongToPayOff);
Console.WriteLine(indentedOutput);