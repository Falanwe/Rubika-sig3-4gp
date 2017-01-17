using CardBattle.Enumerables;
using CardBattle.Models;
using CardBattle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintSyracuse(27);

            Console.ReadLine();
        }

        private static void PrintSyracuse(ulong v)
        {
            for(ulong i = 1; i<100; i++)
            {
                Console.WriteLine($"{i} => {Suites.MaxSyracuseAltitude(i)} {Suites.SyracuseFlightDuration(i)}");
            }
        }

        private static void PrintFibo()
        {
            foreach(var n in Suites.Fibo().Where(i => i % 2 == 0).TakeWhile(i => i < 1000000000000000))
            {
                Console.WriteLine(n);
            }
            Console.WriteLine();

            Console.WriteLine(Suites.Fibo().TakeWhile(i => i < 1000000000000000).Count(i => i % 2 == 0));
        }

        private static void DealCards()
        {
            var dealer = new CardDealer();

            for (var i = 0; i < 26; i++)
            {
                var firstCard = dealer.Deal();
                var secondCard = dealer.Deal();

                Console.WriteLine("{0} {1} {2}".With(firstCard, firstCard.CompareTo(secondCard) > 0 ? ">" : "<", secondCard));

                // Console.WriteLine(String.Format("{0} {1} {2}", firstCard, firstCard.CompareTo(secondCard) > 0 ? ">" : "<", secondCard));
            }
        }

    }
}
