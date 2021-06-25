using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;

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
        /// <param name="value"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static bool Contains<TSource>(
            this IEnumerable<TSource> source, 
            IEnumerable<TSource> value)
        {
            using var e = value.GetEnumerator();
            if (!e.MoveNext())
            {
                return true;
            }

            foreach (TSource s in source)
            {
                if (s.Equals(e.Current))
                {
                    if (!e.MoveNext())
                    {
                        return true;
                    }
                }
                else
                {
                    e.Reset();
                    e.MoveNext();
                    
                    if (!s.Equals(e.Current)) continue;
                    if (!e.MoveNext())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, int>> FindIndexes<TSource>(
            this IEnumerable<TSource> source,
            IEnumerable<TSource> value)
        {
            using var v = value.GetEnumerator();
            using var s = source.GetEnumerator();
            
            if (!v.MoveNext())
            {
                yield break;
            }

            var i = 0;
            var count = value.Count() - 1;

            while (s.MoveNext())
            {
                Debug.Assert(s.Current is not null);

                if (s.Current.Equals(v.Current))
                {
                    if (!v.MoveNext())
                    {
                        yield return new KeyValuePair<int, int>(i - count, i);
                    }
                }
                else
                {
                    v.Reset();
                    v.MoveNext();
                    
                    if (s.Current.Equals(v.Current))
                    {
                        if (!v.MoveNext())
                        {
                            yield return new KeyValuePair<int, int>(i - count, i);
                        }
                    }
                }

                checked
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static TSource MaxSubSum<TSource>(this IEnumerable<TSource> source)
            where TSource : new()
        {
            dynamic i = new TSource();
            dynamic s = new TSource();

            foreach (var m in source)
            {
                i += m;
                if (i < default(TSource)) 
                    i = default(TSource);

                if (i > s)
                {
                    s = i;
                }
            }
            
            return s;
        }
    }
}