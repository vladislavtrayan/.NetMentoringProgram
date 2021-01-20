using System;
using BinaryGap;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(9, 2)]
        [InlineData(529, 4)]
        [InlineData(15, 0)]
        [InlineData(1041, 5)]
        [InlineData(int.MaxValue - 1, 0)]
        [InlineData(int.MaxValue, 0)]
        public void TestOnPositiveNumbers(int n, int expectedBinaryGap)
        {
            Assert.Equal(expectedBinaryGap, BinarySolver.Solve(n));
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(-1)]
        public void NegativeOrZeroNumbersTest(int n)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => BinarySolver.Solve(n));
        }
    }
}