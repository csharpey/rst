using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Rst.Stl.Extensions;

namespace TestRstStl
{
    public class TestSubArrayFindIndexes
    {
        [Fact]
        public void TestEmpty()
        {
            var x = new[] {1, 2, 1, 3, 4, 1, 4, 1, 2, 3, 4};
            var y = Array.Empty<int>();
            
            Assert.Empty(x.FindIndexes(y));
        }
        
        [Theory]
        [ClassData(typeof(EntryOnceTestData))]
        public void TestEntryOnce(int[] x, int[] y, KeyValuePair<int, int>[] expected)
        {
            var actual = x.FindIndexes(y).ToArray();
            
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestEntryTail()
        {
            var x = new[] { 1, 2, 1, 2, 3, 4 };
            var y = new[] { 1, 2, 3 };
            var expected = new KeyValuePair<int, int>[]
            {
                new(2, 4)
            };
            
            var actual = x.FindIndexes(y).ToArray();
            
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestEntryThrice()
        {
            var x = new[] {1, 1, 2, 3, 4, 1, 4, 14, 9, 2, 3, 4, 1, 2, 3, 4};
            var y = new[] {2, 3, 4};
            var expected = new KeyValuePair<int, int>[]
            {
                new(13, 15),
                new(9, 11),
                new(2, 4),
            };
            
            var result = x.FindIndexes(y).ToArray();
            
            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [ClassData(typeof(SubArrayTestData))]
        public void TestEntryN(int[] source, int[] value, int times)
        {
            var actual = source.FindIndexes(value).ToArray();
            Assert.Equal(times, actual.Length);
        }
    }
}