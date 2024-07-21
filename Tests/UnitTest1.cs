// using How_Long_To_Pay_It_Off;
//
// namespace Tests;
//
// public class Tests
// {
//     [SetUp]
//     public void Setup()
//     {
//     }
//
//     [Test]
//     public void CalculateMonthsToPayOff_Test()
//     {
//         // Test case: Loan amount 70000, monthly rate 0.00697 (annual rate 8.364%), monthly payment 500
//         const double loanAmount = 70000;
//         const double monthlyRate = 0.00697;
//         const double payment = 500;
//
//         const int expectedMonths = 276; // Based on provided data: 23 years = 276 months
//         var actualMonths = new Calculator(loanAmount, payment, 60000).GetHowLongToPayItOff(); 
//
//         Assert.That(actualMonths, Is.EqualTo(expectedMonths));
//     }
//
//     [Test]
//     public void ConvertMonthsToYearsMonths_Test()
//     {
//         // Test case: 276 months should be 23 years and 0 months
//         const int totalMonths = 276;
//
//         var (expectedYears, expectedMonths) = (23, 0);
//         var (actualYears, actualMonths) = Calculator.ConvertMonthsToYearsMonths(totalMonths);
//         Assert.Multiple(() =>
//         {
//             Assert.That(actualYears, Is.EqualTo(expectedYears));
//             Assert.That(actualMonths, Is.EqualTo(expectedMonths));
//         });
//     }
//
//     [Test]
//     public void CalculateAnnualInterestRate_Test()
//     {
//         // Test case: Loan amount 70000, monthly payment 500, number of payments 276
//         const double loanAmount = 70000;
//         const double payment = 500;
//         const int numberOfPayments = 276;
//
//         // Using the provided formula to determine expected rate
//         // 0 = 70000 * (1 + 0.00697)^276 - 500 * ((1 + 0.00697)^276 - 1) / 0.00697
//         // Solving for interest rate (0.697% monthly rate, 8.364% annual rate)
//         const double expectedAnnualRate = 0.0869; // In decimal form
//         var actualAnnualRate = Calculator.CalculateAnnualInterestRate(loanAmount, payment, numberOfPayments);
//
//         Assert.That(actualAnnualRate, Is.EqualTo(expectedAnnualRate).Within(0.0001));
//     }
// }

