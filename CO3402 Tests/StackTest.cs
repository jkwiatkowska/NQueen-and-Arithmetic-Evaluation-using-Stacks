using NUnit.Framework;
using CO3402_Assignment;

namespace CO3402_Tests
{
    public class StackTest
    {
        [Test]
        [TestCase(0)]
        [TestCase(10000000000000000000)]
        [TestCase('c')]
        [TestCase("string")]
        [TestCase(-7.5)]
        public void StackBasicTests<T>(T value)
        {
            Stack<T> stack = new Stack<T>();

            Assert.IsTrue(stack.isEmpty());

            stack.push(value);

            Assert.AreEqual(value, stack.top());

            Assert.IsFalse(stack.isEmpty());

            Assert.AreEqual(value, stack.pop());

            Assert.IsTrue(stack.isEmpty());
        }

        [Test]
        [TestCase(0)]
        [TestCase(20)]
        [TestCase(10000)]
        [TestCase(10000000)]
        public void StackSizeTest(int count)
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < count; i++)
            {
                stack.push(0);
            }

            Assert.AreEqual(stack.size(), count);
        }

        [Test]
        [TestCase(2, 5, 0)]
        [TestCase('x', 'y', 'z')]
        public void StackContentTest<T>(T v1, T v2, T v3)
        {
            Stack<T> stack = new Stack<T>(v1);

            Assert.AreEqual(v1, stack.top());

            stack.push(v2);

            Assert.AreEqual(v2, stack.top());

            stack.push(v3);

            Assert.AreEqual(v3, stack.top());

            Assert.AreEqual(stack.ToString(), $"{v3} {v2} {v1} ");

            stack.pop();

            Assert.AreEqual(stack.ToString(), $"{v2} {v1} ");
        }
    }
}