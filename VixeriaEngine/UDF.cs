using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VixeriaEngine
{
    /// <summary>
    /// User Defined Functions.
    /// </summary>
    public static class UDF
    {
        /// <summary>
        /// Returns a string with enumerated array elements.
        /// </summary>
        /// <typeparam name="T">Type of the array.</typeparam>
        /// <param name="array">Array of any type.</param>
        /// <param name="seperator">String to seperate the array elements with.</param>
        public static string ArrayToString<T>(this T[] array, string seperator = ", ")
        { 
            string s = "(";
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i].ToString();
                if (i != array.Length - 1)
                    s += seperator;
                else
                    s += ")";
            }
            if (s == "(")
            {
                s = "null";
            }
            return s;
        }

        /// <summary>
        /// Returns a string with enumerated list elements.
        /// </summary>
        /// <typeparam name="T">Type of the list.</typeparam>
        /// <param name="array">Array of any type.</param>
        /// <param name="seperator">String to seperate the array elements with.</param>
        public static string ListToString<T>(this List<T> list, string seperator = ", ")
        {
            string s = "(";
            for (int i = 0; i < list.Count; i++)
            {
                s += list[i].ToString();
                if (i != list.Count - 1)
                    s += seperator;
                else
                    s += ")";
            }
            if (s == "(")
            {
                s = "null";
            }
            return s;
        }

        /// <summary>
        /// Removes an object at give index and returns it.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="list">List to remove and return from.</param>
        /// <param name="index">Index to remove at.</param>
        /// <returns></returns>
        public static T PopAt<T>(this List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }
    }
}
