using System;
using System.Collections.Generic;
using System.Linq;
using StardewValley;

namespace BetterChestOrganizer
{
    public class ItemComparer : IComparer<Item>
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

        private static int CompareItemQuality(Item a, Item b)
        {
            var aObject = a as StardewValley.Object;
            var bObject = b as StardewValley.Object;
            if (aObject == null)
            {
                if (bObject == null)
                {
                    return 0;
                }

                return -1;
            }

            if (bObject == null)
            {
                return 1;
            }

            return aObject.Quality.CompareTo(bObject.Quality);
        }

        private static int CompareItemColor(Item a, Item b)
        {
            var aObject = a as StardewValley.Objects.ColoredObject;
            var bObject = b as StardewValley.Objects.ColoredObject;
            if (aObject == null)
            {
                if (bObject == null)
                {
                    return 0;
                }

                return -1;
            }

            if (bObject == null)
            {
                return 1;
            }

            var aHue = new Color(aObject.color.R, aObject.color.G, aObject.color.B).H;
            var bHue = new Color(bObject.color.R, bObject.color.G, bObject.color.B).H;
            return aHue.CompareTo(bHue);
        }

        public int Compare(Item a, Item b)
        {
            if (a == null)
            {
                if (b == null)
                {
                    return 0;
                }

                return -1;
            }

            if (b == null)
            {
                return 1;
            }

            var compare = a.getCategorySortValue().CompareTo(b.getCategorySortValue());
            if (compare != 0) return compare;

            compare = CompareItemNames(a.Name, b.Name);
            if (compare != 0) return compare;

            // Compare item ID.
            compare = a.ParentSheetIndex.CompareTo(b.ParentSheetIndex);
            if (compare != 0) return compare;

            compare = CompareItemColor(a, b);
            if (compare != 0) return compare;

            compare = CompareItemQuality(a, b);
            if (compare != 0) return compare;

            compare = a.SpecialVariable.CompareTo(b.SpecialVariable);
            if (compare != 0) return compare;

            compare = a.salePrice().CompareTo(b.salePrice());
            // Largest stack first, intentionally.
            return compare != 0 ? compare : b.Stack.CompareTo(a.Stack);
        }
    }
}