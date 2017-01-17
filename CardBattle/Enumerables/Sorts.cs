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
            throw new NotImplementedException();
        }

        public static List<T> QuickSort<T>(List<T> list) where T : IComparable<T>
        {
            throw new NotImplementedException();
        }
    }
}
