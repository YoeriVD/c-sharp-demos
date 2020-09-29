using System.Collections;
using System.Collections.Generic;
using generics.GenericInterfaces;

namespace generics.GenericMethods
{

    public static class EnumerableHelpers
    {
        public static IEnumerator<T> GetAwesomeEnumerator<T>(this IAwesomeList<T> list)
        {
            for (int i = 0; i < list.Size; i++)
            {
                yield return list[i];
            }
        }
    }
}