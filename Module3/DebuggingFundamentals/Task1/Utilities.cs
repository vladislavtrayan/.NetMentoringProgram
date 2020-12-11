using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Task1
{
    public static class Utilities
    {
        /// <summary>
        /// Sorts an array in ascending order using bubble sort.
        /// </summary>
        /// <param name="numbers">Numbers to sort.</param>
        public static void Sort(int[] numbers)
        {
            if(numbers == null) throw new ArgumentNullException();
            for (var i = 0; i < numbers.Length; i++)
            {
                for (var j = 0; j < numbers.Length - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        var temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
        }
        
        /// <summary>
        /// Sorts an array in ascending order using bubble sort.
        /// </summary>
        public static void Sort<T>(this T[] items) where T : IComparable
        {
            if(items == null)
                throw new ArgumentNullException();
            for (var i = 0; i < items.Length; i++)
            {
                for (var j = 0; j < items.Length - 1; j++)
                {
                    if (items[j].CompareTo(items[j + 1]) >= 0)
                    {
                        var temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }
        
        public static void Sort<T>(this T[] items, Func<T,T,bool> comparator)
        {
            if(items == null || comparator == null)
                throw new ArgumentNullException();
            
            for (var i = 0; i < items.Count(); i++)
            {
                for (var j = 0; j < items.Count() - 1; j++)
                {
                    if (comparator((items[j]),(items[j + 1])))
                    {
                        var temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Searches for the index of a product in an <paramref name="products"/> 
        /// based on a <paramref name="predicate"/>.
        /// </summary>
        /// <param name="products">Products used for searching.</param>
        /// <param name="predicate">Product predicate.</param>
        /// <returns>If match found then returns index of product in <paramref name="products"/>
        /// otherwise -1.</returns>
        public static int IndexOf(Product[] products, Predicate<Product> predicate)
        {
            if(products == null || predicate == null)
                throw new ArgumentNullException();

            for (var i = 0; i < products.Length; i++)
            {
                var product = products[i];
                if (predicate(product))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public static int IndexOf<T>(T[] elements, Predicate<T> predicate)
        {
            if(elements == null || predicate == null)
                throw new ArgumentNullException();

            for (var i = 0; i < elements.Length; i++)
            {
                if (predicate(elements[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
