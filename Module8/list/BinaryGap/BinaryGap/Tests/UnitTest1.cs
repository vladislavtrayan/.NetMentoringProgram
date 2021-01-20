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
        [InlineData(1111, 0)]
        [InlineData(1041, 5)]
        public void Test1(int n, int expectedBinaryGap)
        {
            Assert.Equal(expectedBinaryGap, BinarySolver.Solve(n));
        }
    }
}