using System;
using System.Linq;
using Rst.Stl.Extensions;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

namespace Rst.Stl.Benchmark
{
    public class MaxSubSumGenericVsIntSpec
    {
        private const int N = 100;
        private readonly int[] _data;

        public MaxSubSumGenericVsIntSpec()
        {
            var provider = new RNGCryptoServiceProvider();
            var bytes = new byte[N];
            provider.GetBytes(bytes);
            _data = Array.ConvertAll(bytes, b => (int) b);
        }

        [Benchmark]
        public int Generic()
        {
            return _data.MaxSubSum<int>();
        }

        [Benchmark]
        public int Spec()
        {
            return _data.MaxSubSum();
        }
    }
}