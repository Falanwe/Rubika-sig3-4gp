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
            var dealer = new CardDealer();

          

            for(var i = 0; i<100; i++)
            {
                var firstCard = dealer.Deal();
                var secondCard = dealer.Deal();
                Console.WriteLine($"{firstCard} {(firstCard.CompareTo(secondCard) > 0? ">" :"<")} {secondCard}");
            }

            Console.ReadLine();
        }

        
    }
}
