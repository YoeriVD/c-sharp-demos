using System.Collections.Generic;
using generics.GenericInterfaces;

namespace generics.GenericMethods
{
    public static class EnumerableHelpers
    {
        public static IEnumerator<T> GetAwesomeEnumerator<T>(this IAwesomeList<T> list)
        {
            for (var i = 0; i < list.Size; i++)
            {
                yield return list[i];
            }
        }


        public static IEnumerable<T> SortAsNew<T>(this IEnumerable<T> list)
        {
            var tempList = new List<T>(list);
            tempList.Sort();
            return tempList;
        }
    }
}