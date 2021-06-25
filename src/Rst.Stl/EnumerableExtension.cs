using System;
using System.Collections.Generic;
        
namespace Rst.Stl.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class EnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Double MaxSubSum(this IEnumerable<Double> source)
        {
            Double i = 0;
            Double s = 0;

            foreach (var m in source)
            {
                i += m;
                if (i < 0) 
                    i = 0;

                if (i > s)
                {
                    s = i;
                }
            }
            
            return s;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Int32 MaxSubSum(this IEnumerable<Int32> source)
        {
            Int32 i = 0;
            Int32 s = 0;

            foreach (var m in source)
            {
                i += m;
                if (i < 0) 
                    i = 0;

                if (i > s)
                {
                    s = i;
                }
            }
            
            return s;
        }
    }
}
