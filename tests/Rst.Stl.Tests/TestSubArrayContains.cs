using System;
using Rst.Stl.Extensions;
using Xunit;

namespace Rst.Stl.Tests
{
    public class TestSubArrayContains
    {
        [Fact]
        public void TestEmpty()
        {
            int[] x = { 1, 2, 1, 3, 4, 1, 4, 1, 2, 3, 4 };
            var y = Array.Empty<int>();
            
            Assert.True(x.Contains(y));
        }
        
        [Theory]
        [ClassData(typeof(SelfContainsTestData))]
        public void TestSelfContains(int[] x)
        {
            Assert.True(x.Contains(x));
        }
        
        [Theory]
        [InlineData(new[]{ 1, 2, 1, 3, 4, 1, 4, 1, 2, 3, 4 }, new[]{ 2, 3, 4 })]
        [InlineData(new[]{ 1, 2, 1, 2, 3, 4 }, new[]{ 1, 2, 3 })]
        [InlineData(new[]{ 1, 2, 1, 3, 4, 1, 4, 1, 2, 3, 4 }, new[]{ 2 })]
        [InlineData(new[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 
        }, new[]{ 9, 10, 11, 12, 1, 2, 3, 4, 5 })]
        public void TestContains(int[] x, int[] y)
        {
            Assert.True(x.Contains(y));
        }
        
        [Theory]
        [InlineData(new[]{ 1, 2, 1, 3, 4, 1, 4, 1 }, new[]{ 2, 3, 4 })]
        [InlineData(new[]{ 1, 1, 3, 4, 1, 4, 1, 3, 4 }, new[]{ 2 })]
        [InlineData(new[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 
        }, new[]{ 9, 10, 11, 1, 2, 3, 4, 5 })]
        public void TestNotContains(int[] x, int[] y)
        {
            Assert.False(x.Contains(y));
        }
    }
}