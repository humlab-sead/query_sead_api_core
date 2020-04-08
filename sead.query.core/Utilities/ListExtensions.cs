﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SeadQueryCore
{
    public static class ListExtensions
    {
        public static List<string> AddIfMissing(this List<string> array, string element)
        {
            if (element != null && !array.Contains(element))
                array.Add(element);
            return array;
        }

        public static IEnumerable<string> AppendIf(this IEnumerable<string> array, string element)
        {
            return element.IsEmpty() ? array : array.Append(element);
        }

        public static string Combine(this List<string> array, string glue)
        {
            return String.Join(glue, array);
        }

        public static string Combine<T>(this List<T> array, string glue, Func<T, string> selector)
        {
            return String.Join(glue, array.Select(selector).ToList());
        }

        public static string Combine<T>(this List<T> array, string glue = "", string prefix = "", string suffix = "", string default_value = "")
        {
            return String.Join(glue, array.Select(x => $"{prefix}{x.ToString() ?? default_value}{suffix}").ToList());
        }

        public static void InsertAt<T>(this List<T> array, T itemToFind, T itemToInsert)
        {
            var idx = array.IndexOf(itemToFind);

            if (idx < 0)
                throw new ArgumentException($"List<T>.InsertAt: {itemToFind} to found");

            array.Insert(idx, itemToInsert);
        }
    }
}
