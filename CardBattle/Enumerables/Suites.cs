using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Enumerables
{
    public static class Suites
    {
        public static IEnumerable<ulong> Fibo()
        {
            yield return 1;
            yield return 1;

            ulong current = 1;
            ulong prev = 1;

            while (true)
            {
                var sum = current + prev;

                yield return sum;

                prev = current;
                current = sum;
            }
        }

        public static IEnumerable<ulong> Syracuse(ulong start)
        {
            if(start == 0)
            {
                throw new ArgumentException("0 is not a valid starting point");
            }

            var current = start;

            while (true)
            {
                yield return current;

                if(current %2 == 0)
                {
                    current /= 2;
                }
                else
                {
                    current = current * 3 + 1;
                }
            }
        }

        public static ulong MaxSyracuseAltitude(ulong start)
        {
            return Syracuse(start).TakeWhile(i => i != 1).DefaultIfEmpty(start).Max();
        }

        public static int SyracuseFlightDuration(ulong start)
        {
            return Syracuse(start).TakeWhile(i => i != 1).Count();
        }
    }
}
