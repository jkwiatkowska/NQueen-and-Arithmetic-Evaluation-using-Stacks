using NUnit.Framework;
using CO3402_Assignment;

namespace CO3402_Tests
{
    class ExpressionEvaluationTest
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("(", false)]
        [TestCase(")", false)]
        [TestCase("+", false)]
        [TestCase("()", false)]
        [TestCase("1/", false)]
        [TestCase("*41", false)]
        [TestCase("2++", false)]
        [TestCase("1**4", false)]
        [TestCase("1+4-)8^2(", false)]
        [TestCase("1234", true)]
        [TestCase("1+1     ", true)]
        [TestCase(" 2 * 7-1", true)]
        [TestCase("9^(7%2)", true)]
        [TestCase("1*(8-(2+4))", true)]
        public void ValidateExpressionTest(string expression, bool expectedResult)
        {
            bool result = ExpressionEvaluation.ValidateExpression(expression);
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        [TestCase("2+2", "2 2 +")]
        [TestCase("102%2", "102 2 %")]
        [TestCase("2*3/(2-1)+5*3", "2 3 * 2 1 - / 5 3 * +")]
        [TestCase("P+(Q*R)/(S-T)", "P Q R * S T - / +")]
        [TestCase("5A+(B*C-(D/E^F)*9G)*11H", "5A B C * D E F ^ / 9G * - 11H * +")]
        public void InfixToPostfixTest(string infix, string postfix)
        {
            string result = ExpressionEvaluation.InfixToPostfix(infix);
            Assert.AreEqual(postfix, result);
        }

        [Test]
        [TestCase("2+2", "+ 2 2")]
        [TestCase("102%2", "% 102 2")]
        [TestCase("2*3/(2-1)+5*3", "+ * 2 / 3 - 2 1 * 5 3")]
        [TestCase("P+(Q*R)/(S-T)", "+ P / * Q R - S T")]
        [TestCase("5A+(B*C-(D/E^F)*9G)*11H", "+ 5A * - * B C * / D ^ E F 9G 11H")]
        public void InfixToPrefixTest(string infix, string prefix)
        {
            string result = ExpressionEvaluation.InfixToPrefix(infix);
            Assert.AreEqual(prefix, result);
        }

        [Test]
        [TestCase("2+2", 4.0f)]
        [TestCase("1-2", -1.0f)]
        [TestCase("10%3", 1.0f)]
        [TestCase("2^3", 8.0f)]
        [TestCase("5/2", 2.5f)]
        [TestCase("2^2^3", 256.0f)]
        [TestCase("(2^3)^3", 512.0f)]
        [TestCase("1/10000", 0.0001f)]
        [TestCase("2*3/(2-1)+5*3", 21.0f)]
        [TestCase("2*10^2%(4-1)", 2.0f)]
        public void EvaluationTest(string infix, float expectedValue)
        {
            float result;
            ExpressionEvaluation.TryEvaluate(ExpressionEvaluation.InfixToPrefix(infix), out result);

            Assert.AreEqual(expectedValue, result);
        }
    }
}
