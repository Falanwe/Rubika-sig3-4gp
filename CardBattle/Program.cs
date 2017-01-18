using Autofac;
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
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CardDealer>();

            var logger = new ConsoleLogger();
            logger.MinLevel = LogLevel.Debug;

            containerBuilder.RegisterInstance(logger).As<ILogger>();

            var container = containerBuilder.Build();

            var dealer = container.Resolve<CardDealer>();

            Console.WriteLine(dealer.Deal());

            Console.ReadLine();
        }

        //private static void TestSorts()
        //{
        //    var dealer = new CardDealer();


        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();
        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();
        //        deck.Sort();
        //    }

        //    Console.WriteLine("list.sort: " + Card.ComparisonCount);
        //    Card.ResetComparisonCount();


        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();
        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();
        //        deck = deck.OrderBy(_ => _).ToList();
        //    }

        //    Console.WriteLine("linq: " + Card.ComparisonCount);
        //    Card.ResetComparisonCount();

        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();
        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();
        //        Sorts.InsertionSort(deck);
        //    }

        //    Console.WriteLine("insertion: " + Card.ComparisonCount);

        //    Card.ResetComparisonCount();

        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();
        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();

        //        Sorts.BubleSort(deck);
        //    }

        //    Console.WriteLine("bubble: " + Card.ComparisonCount);


        //    Card.ResetComparisonCount();

        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();
        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();

        //        Sorts.ShakeSort(deck);
        //    }

        //    Console.WriteLine("shake: " + Card.ComparisonCount);
        //    Card.ResetComparisonCount();

        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();

        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();

        //        deck = Sorts.MergeSort(deck);
        //    }

        //    Console.WriteLine("merge: " + Card.ComparisonCount);
        //    Card.ResetComparisonCount();

        //    for (var i = 0; i < 1000; i++)
        //    {
        //        dealer.Shuffle();

        //        var deck = dealer.Deal(52).ToList();
        //        deck = deck.Concat(deck).ToList();

        //        deck = Sorts.QuickSort(deck);
        //    }

        //    Console.WriteLine("quick: " + Card.ComparisonCount);

        //}

        private static void PrintSyracuse(ulong v)
        {
            for (ulong i = 1; i < 100; i++)
            {
                Console.WriteLine($"{i} => {Suites.MaxSyracuseAltitude(i)} {Suites.SyracuseFlightDuration(i)}");
            }
        }

        private static void PrintFibo()
        {
            foreach (var n in Suites.Fibo().Where(i => i % 2 == 0).TakeWhile(i => i < 1000000000000000))
            {
                Console.WriteLine(n);
            }
            Console.WriteLine();

            Console.WriteLine(Suites.Fibo().TakeWhile(i => i < 1000000000000000).Count(i => i % 2 == 0));
        }

        //private static void DealCards()
        //{
        //    var dealer = new CardDealer();

        //    for (var i = 0; i < 26; i++)
        //    {
        //        var firstCard = dealer.Deal();
        //        var secondCard = dealer.Deal();

        //        Console.WriteLine("{0} {1} {2}".With(firstCard, firstCard.CompareTo(secondCard) > 0 ? ">" : "<", secondCard));

        //        // Console.WriteLine(String.Format("{0} {1} {2}", firstCard, firstCard.CompareTo(secondCard) > 0 ? ">" : "<", secondCard));
        //    }
        //}

    }
}
