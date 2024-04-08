using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4
{
    public static class LINQExtension
    {
        public static TSource MedianElement<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any())
            {
                throw new ArgumentNullException("Collection is null or empty!");
            }
            else
            {
                if (source.Count() % 2 != 0)
                {
                    return source.ElementAt(source.Count() / 2);
                }
                else
                {
                    return source.ElementAt(source.Count() / 2 - 1);
                }
            }
        }
    }
}
