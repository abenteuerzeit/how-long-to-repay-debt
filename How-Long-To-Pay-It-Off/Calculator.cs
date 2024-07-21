using System.Globalization;
using System.Text;

namespace How_Long_To_Pay_It_Off;

/// <summary>
///     Represents a financial calculator for loan repayment scenarios.
/// </summary>
public class Calculator
{
    private readonly CultureInfo _culture;
    private readonly decimal _loanAmount;
    private readonly decimal _monthlyRate;
    private readonly int _numberOfMonths;
    private readonly decimal _payment;

    /// <summary>
    ///     Initializes a new instance of the Calculator class.
    /// </summary>
    /// <param name="loanAmount">The initial loan amount.</param>
    /// <param name="payment">The monthly payment amount.</param>
    /// <param name="numberOfMonths">The total number of months for the loan term.</param>
    /// <param name="remainingLoan">The remaining loan amount after the specified number of months.</param>
    /// <param name="culture">The culture info for formatting. Defaults to en-US if not specified.</param>
    public Calculator(decimal loanAmount, decimal payment, int numberOfMonths, decimal remainingLoan,
        CultureInfo? culture = null)
    {
        _culture = culture ?? new CultureInfo("en-US");
        _loanAmount = loanAmount;
        _monthlyRate = GetMonthlyInterestRate(loanAmount, payment, numberOfMonths, remainingLoan);
        _payment = payment;
        _numberOfMonths = numberOfMonths;
    }

    /// <summary>
    ///     Calculates the future value of the loan.
    /// </summary>
    /// <returns>The future value of the loan.</returns>
    public decimal GetFutureValue()
    {
        return GetFutureValue(_payment);
    }

    /// <summary>
    ///     Calculates the future value of the loan with a specified payment.
    /// </summary>
    /// <param name="payment">The payment amount to use in the calculation.</param>
    /// <returns>The future value of the loan.</returns>
    private decimal GetFutureValue(decimal payment)
    {
        return GetCompoundInterestGrowth(_loanAmount, _monthlyRate, _numberOfMonths) -
               GetAnnuity(payment, _monthlyRate, _numberOfMonths);
    }

    /// <summary>
    ///     Calculates the effective annual interest rate.
    /// </summary>
    /// <returns>The effective annual interest rate.</returns>
    public decimal GetEffectiveAnnualInterestRate()
    {
        return Power(1 + _monthlyRate, 12) - 1;
    }

    /// <summary>
    ///     Calculates the monthly interest rate based on the loan parameters.
    /// </summary>
    /// <param name="initialLoan">The initial loan amount.</param>
    /// <param name="payment">The monthly payment amount.</param>
    /// <param name="months">The number of months for the loan term.</param>
    /// <param name="remainingLoan">The remaining loan amount after the specified number of months.</param>
    /// <returns>The calculated monthly interest rate.</returns>
    /// <exception cref="Exception">Thrown when the calculation fails to converge.</exception>
    public static decimal GetMonthlyInterestRate(decimal initialLoan, decimal payment, int months,
        decimal remainingLoan)
    {
        const decimal tolerance = 1e-10m;
        const int maxIterations = 100;
        var rate = 0.01m;

        for (var i = 0; i < maxIterations; i++)
        {
            var fValue = F(rate);
            var fPrimeValue = FPrime(rate);

            if (fPrimeValue == 0) throw new Exception("Derivative is zero; failed to converge.");

            var rNew = rate - fValue / fPrimeValue;
            if (Math.Abs(rNew - rate) < tolerance) return rNew;

            rate = rNew;
        }

        throw new Exception("Failed to converge on an interest rate.");

        decimal FPrime(decimal r)
        {
            var growthFactor = Power(1 + r, months);
            var annuityFactor = (growthFactor - 1) / r;

            return initialLoan * months * growthFactor / (1 + r) -
                   payment * ((months * growthFactor / (1 + r) * r - annuityFactor) / r);
        }

        decimal F(decimal r)
        {
            return GetCompoundInterestGrowth(initialLoan, r, months) -
                   GetAnnuity(payment, r, months) -
                   remainingLoan;
        }
    }

    /// <summary>
    ///     Generates a payment table showing different payment scenarios.
    /// </summary>
    /// <returns>A string representation of the payment table.</returns>
    public string GetPayTable()
    {
        const int paymentStart = 500;
        const int paymentEnd = 900;
        const int paymentStep = 100;
        const int columnCount = 5;

        var sb = new StringBuilder();
        int[] columnWidths = [10, 15, 10, 10, 10];
        string[] headers = ["Payment", "Future Value", "Years", "Months", "Total Months"];

        for (var i = 0; i < columnCount; i++)
        {
            sb.Append(headers[i].PadRight(columnWidths[i]));
            if (i < columnCount - 1) sb.Append(' ');
        }

        sb.AppendLine()
            .AppendLine(new string('-', sb.Length - Environment.NewLine.Length));

        for (var payment = paymentStart; payment <= paymentEnd; payment += paymentStep)
        {
            decimal paymentDecimal = payment;
            var futureValue = GetFutureValue(paymentDecimal);
            var totalMonths = Math.Round(CalculateNumberOfPayments(_loanAmount, _monthlyRate, paymentDecimal), 1);
            var years = (int)Math.Floor(totalMonths / 12);
            var months = (int)(totalMonths % 12);

            sb.Append(paymentDecimal.ToString("C", _culture).PadRight(columnWidths[0])).Append(' ')
                .Append(futureValue.ToString("C", _culture).PadRight(columnWidths[1])).Append(' ')
                .Append(years.ToString(_culture).PadRight(columnWidths[2])).Append(' ')
                .Append(months.ToString(_culture).PadRight(columnWidths[3])).Append(' ')
                .AppendLine(totalMonths.ToString(_culture).PadRight(columnWidths[4]));
        }

        return sb.ToString();
    }

    /// <summary>
    ///     Calculates the compound interest growth.
    /// </summary>
    private static decimal GetCompoundInterestGrowth(decimal pv, decimal r, int n)
    {
        return pv * Power(1 + r, n);
    }

    /// <summary>
    ///     Calculates the annuity value.
    /// </summary>
    private static decimal GetAnnuity(decimal p, decimal r, int n)
    {
        return p * ((Power(1 + r, n) - 1) / r);
    }

    /// <summary>
    ///     Calculates the number of payments required to pay off a loan.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when input parameters are invalid.</exception>
    private static decimal CalculateNumberOfPayments(decimal loanAmount, decimal interestRate, decimal payment)
    {
        if (interestRate <= 0 || payment <= 0 || loanAmount <= 0 || payment <= loanAmount * interestRate)
            throw new ArgumentException("Invalid input parameters.");

        return (decimal)Math.Log((double)(payment / (payment - loanAmount * interestRate))) /
               (decimal)Math.Log((double)(1 + interestRate));
    }

    /// <summary>
    ///     Raises a decimal base to an integer exponent.
    /// </summary>
    private static decimal Power(decimal baseValue, int exponent)
    {
        decimal result = 1;
        for (var i = 0; i < exponent; i++) result *= baseValue;
        return result;
    }
}