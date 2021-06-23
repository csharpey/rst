using System.Collections;
using System.Collections.Generic;

namespace TestRstStl
{
    public class EntryOnceTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new[] {1, 2, 1, 3, 4, 1, 4, 1, 2, 3, 4},
                new[] {2, 3, 4},
                new KeyValuePair<int, int>[]
                {
                    new(8, 10)
                }
            };
            yield return new object[] {
                new[] {1, 2, 1, 3, 4, 1, 4, 1, 2, 3, 4},
                new[] {2},
                new KeyValuePair<int, int>[]
                {
                    new(8, 8),
                    new(1, 1),
                }
            };
            yield return new object[] {
                new[] {1, 2, 1, 2, 3, 4},
                new[] {1, 2, 3},
                new KeyValuePair<int, int>[]
                {
                    new(2, 4),
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}