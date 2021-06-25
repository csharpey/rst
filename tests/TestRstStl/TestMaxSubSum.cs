using System;
using Rst.Stl.Extensions;
using Xunit;

namespace TestRstStl
{
    public class TestMaxSubSum
    {
        [Fact]
        public void TestGeneric()
        {
            var x = new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            const int expected = 6;
            
            Assert.Equal(expected, x.MaxSubSum<int>());
            Assert.Equal(expected, x.MaxSubSum());
        }
        
        [Fact]
        public void TestGenericDouble()
        {
            double[] x = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            const int expected = 6;
            
            Assert.Equal(expected, x.MaxSubSum<double>());
            Assert.Equal(expected, x.MaxSubSum());
        }
    }
}