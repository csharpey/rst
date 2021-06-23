using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace TestRstStl
{
    public class SubArrayTestData : IEnumerable<object[]>
    {
        private RNGCryptoServiceProvider _rnd = new();
        public IEnumerator<object[]> GetEnumerator()
        {
            var random = new Random();
            
            var times = random.Next(1, 10);
            
            for (int i = 0; i < times; i++)
            {
                var value = RandomArray(4);
                var source = Enumerable.Repeat(value, times)
                    .Select(i => RandomArray(random.Next(1, 10)).Concat(i))
                    .SelectMany(i => i)
                    .ToArray();

                yield return new object[] { source, value, times };
            }
        }

        private int[] RandomArray(int s)
        {
            var bytes = new byte[s];
            _rnd.GetBytes(bytes);
            return Array.ConvertAll(bytes, b => (int) b);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}