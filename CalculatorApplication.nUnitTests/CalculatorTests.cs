namespace CalculatorApplication.nUnitTests
{
    public class CalculatorTests
    {
        Calculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator();
        }


        [Test]
        public void validate_empty_input()
        {
            bool result = calculator.validateInput("");
            Assert.IsFalse(result);
        }

        [Test]
        public void validate_input_greater_than_int_maxvalue()
        {
            bool result = calculator.validateInput("add(1,214748364799)");
            Assert.IsFalse(result);
        }

        [Test]
        public void validate_input_less_than_int_minvalue()
        {
            bool result = calculator.validateInput("add(1,-214748364799)");
            Assert.IsFalse(result);
        }

        [Test]
        public void validate_input_correct_format()
        {
            bool result = calculator.validateInput("add(1,2)");
            Assert.IsTrue(result);
        }

        [Test]
        public void validate_input_invalid_function_format()
        {
            bool result = calculator.validateInput("add(1,subbb(1,2))");
            Assert.IsFalse(result);
        }

        [Test]
        public void validate_input_missing_braces_format()
        {
            bool result = calculator.validateInput("add(1,sub(1,2))))");
            Assert.IsFalse(result);
        }
    }
}