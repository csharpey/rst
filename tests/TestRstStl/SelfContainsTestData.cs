using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace TestRstStl
{
    public class SelfContainsTestData : IEnumerable<object[]>
    {
        private RNGCryptoServiceProvider _rnd = new();
        public IEnumerator<object[]> GetEnumerator()
        {
            var size = new Random().Next(10);
            for (int i = 0; i < size; i++)
            {
                var bytes = new byte[100];
                _rnd.GetBytes(bytes);
                yield return new object[] { Array.ConvertAll(bytes, b => (int)b) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}