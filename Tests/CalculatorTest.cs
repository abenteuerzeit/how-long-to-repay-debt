using System.Globalization;
using How_Long_To_Pay_It_Off;

namespace Tests;

[TestFixture]
public class CalculatorTests
{
    [SetUp]
    public void Setup()
    {
        _calculator = new Calculator(InitialLoan, Payment, MonthsPaid, RemainingLoan);
    }

    private const decimal InitialLoan = 70000;
    private const decimal Payment = 500;
    private const int MonthsPaid = 276;
    private const decimal RemainingLoan = 60000;
    private const decimal Tolerance = 0.1m;

    private Calculator _calculator;

    [Test]
    public void GetMonthlyInterestRate_GivenLoanDetails_ReturnsCorrectRate()
    {
        var monthlyRate = Calculator.GetMonthlyInterestRate(InitialLoan, Payment, MonthsPaid, RemainingLoan);
        const decimal expectedRate = 0.006971245281m;
        Assert.That(monthlyRate, Is.EqualTo(expectedRate).Within(Tolerance));
    }

    [Test]
    public void GetEffectiveAnnualInterestRate_GivenMonthlyRate_ReturnsCorrectRate()
    {
        var annualRate = _calculator.GetEffectiveAnnualInterestRate();
        var monthlyRate = Calculator.GetMonthlyInterestRate(InitialLoan, Payment, MonthsPaid, RemainingLoan);
        var expectedRate = (decimal)Math.Pow((double)(1 + monthlyRate), 12) - 1;
        Assert.That(annualRate, Is.EqualTo(expectedRate).Within(Tolerance));
    }

    [Test]
    public void GetFutureValue_GivenLoanDetails_ReturnsCorrectValue()
    {
        var futureValue = _calculator.GetFutureValue();
        Assert.That(futureValue, Is.EqualTo(RemainingLoan).Within(Tolerance));
    }

    [TestCase(500, 537)]
    [TestCase(600, 242)]
    [TestCase(700, 172)]
    [TestCase(800, 136)]
    [TestCase(900, 113)]
    public void GetPayTable_GivenDifferentPayments_ReturnsCorrectMonths(decimal payment, int expectedMonths)
    {
        var payTable = _calculator.GetPayTable();
        var c = new CultureInfo("en-US");

        var lines = payTable.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
        var targetLine = lines.FirstOrDefault(line => line.Contains(payment.ToString("C", c)));

        Assert.That(targetLine, Is.Not.Null, $"Payment line not found for payment: {payment.ToString("C", c)}");

        var columns = targetLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var totalMonths = decimal.Parse(columns.Last(), c);

        Assert.That(totalMonths, Is.EqualTo(expectedMonths).Within(1));
    }
}