using System;
using System.Collections.Generic;
using System.Text;

namespace StockSolution.Model
{
    public static class IEnumeratorHelper
    {
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}
