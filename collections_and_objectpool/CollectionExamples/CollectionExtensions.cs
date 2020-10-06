using System;
using System.Collections;
using System.Collections.Generic;

namespace collections_and_objectpool.CollectionExamples
{
    public static class CollectionExtensions
    {
        public static void Print<T, K>(this IDictionary<T, K> dictionary) where K : IEnumerable
        {
            foreach (var key in dictionary.Keys)
            {
                Console.WriteLine(key);
                foreach (var person in dictionary[key])
                {
                    Console.WriteLine("\t" + person);
                }
            }
        }

        public static void Print<T>(this IEnumerable<T> collection)
        {
            foreach (var person in collection)
            {
                Console.WriteLine(person);
            }
        }
    }
}