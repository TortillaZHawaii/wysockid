using NUnit.Framework;
using StringCalculator;
using System;

namespace StringCalculator.Test
{
    public class CalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmptyStringReturns0Test()
        {
            var calc = new Calculator();
            string input = string.Empty;

            int result = calc.GetResult(input);
            int expected = 0;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SingleNumberReturnsNumberTest()
        {
            var calc = new Calculator();
            string input = 46.ToString();

            int result = calc.GetResult(input);
            int expected = 46;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TwoNumbersSplitByComaReturnsSumTest()
        {
            var calc = new Calculator();
            string input = "46,132";

            int result = calc.GetResult(input);
            int expected = 178;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TwoNumbersSplitByNewlineReturnsSumTest()
        {
            var calc = new Calculator();
            string input = "46\n132";

            int result = calc.GetResult(input);
            int expected = 178;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThreeNumbersSplitByNewlineOrComaReturnSumTest()
        {
            var calc = new Calculator();
            string input = "46\n132,5";

            int result = calc.GetResult(input);
            int expected = 183;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NegativeNumbersThrowExceptionTest()
        {
            var calc = new Calculator();
            string input = "46\n-132,5";

            Assert.Throws<ArgumentException>(() => calc.GetResult(input));
        }

        [Test]
        public void NumbersLargerThan1000AreIgnoredTest()
        {
            var calc = new Calculator();
            string input = "46\n1001,5";

            int result = calc.GetResult(input);
            int expected = 51;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CustomSeparatorTest()
        {
            var calc = new Calculator();
            string input = "#a46\n132,5";

            int result = calc.GetResult(input);
            int expected = 183;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CustomSeparatorInBracketsTest()
        {
            var calc = new Calculator();
            string input = "#[abc]46\n132abc5";

            int result = calc.GetResult(input);
            int expected = 183;

            Assert.AreEqual(expected, result);
        }
    }
}