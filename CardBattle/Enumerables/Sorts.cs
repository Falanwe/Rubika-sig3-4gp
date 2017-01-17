using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Enumerables
{
    public static class Sorts
    {
        private static bool SwapIfNeeded<T>(List<T> list, int i, int j) where T : IComparable<T>
        {
            if (list[i].CompareTo(list[j]) > 0)
            {
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
                return true;
            }
            return false;
        }

        public static List<T> InsertionSort<T>(List<T> list) where T : IComparable<T>
        {
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    SwapIfNeeded(list, i, j);
                }
            }

            return list;
        }

        public static List<T> BubleSort<T>(List<T> list) where T : IComparable<T>
        {
            for (var i = list.Count - 1; i > 2; i--)
            {
                var sorted = true;
                for (var j = 0; j < i; j++)
                {
                    sorted &= !SwapIfNeeded(list, j, j + 1);
                }
                if (sorted)
                {
                    break;
                }
            }
            return list;
        }

        public static List<T> ShakeSort<T>(List<T> list) where T : IComparable<T>
        {
            for (var i = 0; i < list.Count / 2; i++)
            {
                var sorted = true;

                for (var j = i; j < list.Count - i - 1; j++)
                {
                    sorted &= SwapIfNeeded(list, j, j + 1);
                }
                if (sorted)
                {
                    break;
                }
                sorted = true;
                for (var j = list.Count - i - 3; j >= i; j--)
                {
                    sorted &= SwapIfNeeded(list, j, j + 1);
                }
                if (sorted)
                {
                    break;
                }
            }
            return list;
        }

        public static List<T> MergeSort<T>(List<T> list) where T : IComparable<T>
        {
            if (list.Count <= 1)
            {
                return list;
            }
            var half = list.Count / 2;
            var left = MergeSort(list.GetRange(0, half));
            var right = MergeSort(list.GetRange(half, list.Count - half));

            return Merge(left, right);
        }

        private static List<T> Merge<T>(List<T> left, List<T> right) where T : IComparable<T>
        {
            var result = new List<T>();

            var leftIndex = 0;
            var rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (left[leftIndex].CompareTo(right[rightIndex]) > 0)
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
                else
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
            }

            if (leftIndex == left.Count)
            {
                result.AddRange(right.GetRange(rightIndex, right.Count - rightIndex));
            }
            else
            {
                result.AddRange(left.GetRange(leftIndex, left.Count - leftIndex));
            }

            return result;
        }

        public static List<T> QuickSort<T>(List<T> list) where T : IComparable<T>
        {
            if (list.Count <= 1)
            {
                return list;
            }

            var pivot = list[0];
            var left = new List<T>();
            var right = new List<T>();

            foreach (var element in list.Skip(1))
            {
                if (pivot.CompareTo(element) > 0)
                {
                    left.Add(element);
                }
                else
                {
                    right.Add(element);
                }
            }

            left = QuickSort(left);
            right = QuickSort(right);

            left.Add(pivot);
            left.AddRange(right);

            return left;
        }
    }
}
