using System;
using System.Collections.Generic;
using System.Linq;
using StardewValley;

namespace BetterChestOrganizer
{
    public static class ItemComparator
    {
        private static readonly string[] CategoricalSuffixes = {"Jelly", "Cheese", "Juice", "Wine"};

        private static void RotateRight(IList<string> array)
        {
            var last = array.Last();
            for (var i = 1; i < array.Count; i++)
            {
                array[i] = array[i - 1];
            }

            array[0] = last;
        }

        private static string[] GetTokenizedName(string name)
        {
            var tokens = name.Split();
            if (tokens.Length == 0) return tokens;
            if (CategoricalSuffixes.Contains(tokens.Last()))
            {
                RotateRight(tokens);
            }

            return tokens;
        }

        private static int CompareArrays<T>(ref T[] a, ref T[] b) where T : IComparable
        {
            for (var i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                if (i < a.Length && i < b.Length)
                {
                    var compareResult = a[i].CompareTo(b[i]);
                    if (compareResult != 0)
                    {
                        return compareResult;
                    }
                }
                else if (i == a.Length)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }

        private static int CompareItemNames(string a, string b)
        {
            if (a == b) return 0;
            var aTokens = GetTokenizedName(a);
            var bTokens = GetTokenizedName(b);
            return CompareArrays(ref aTokens, ref bTokens);
        }

        public static int CompareItems(Item a, Item b)
        {
            var compare = a.getCategorySortValue().CompareTo(b.getCategorySortValue());
            if (compare != 0)
            {
                return compare;
            }

            compare = CompareItemNames(a.Name, b.Name);
            if (compare != 0)
            {
                return compare;
            }

            compare = a.ParentSheetIndex.CompareTo(b.ParentSheetIndex);
            if (compare != 0)
            {
                return compare;
            }

            compare = a.SpecialVariable.CompareTo(b.SpecialVariable);
            if (compare != 0)
            {
                return compare;
            }

            compare = a.salePrice().CompareTo(b.salePrice());
            // Largest stack first, intentionally.
            return compare != 0 ? compare : b.Stack.CompareTo(a.Stack);
        }
    }
}