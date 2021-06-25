using System;
using System.Linq;
using Rst.Stl.Extensions;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

namespace Rst.Stl.Benchmark
{
    public class MaxSubSumGenericVsDoubleSpec
    {
        private const int N = 100;
        private readonly double[] _data;

        public MaxSubSumGenericVsDoubleSpec()
        {
            var provider = new RNGCryptoServiceProvider();
            var bytes = new byte[N];
            provider.GetBytes(bytes);
            _data = Array.ConvertAll(bytes, b => (double) b);
        }

        [Benchmark]
        public double Generic()
        {
            return _data.MaxSubSum<double>();
        }

        [Benchmark]
        public double Spec()
        {
            return _data.MaxSubSum();
        }
    }
}