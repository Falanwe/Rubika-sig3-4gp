using CardBattle.Models;
using CardBattle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Player
{
    public class PierrePlayer : IPlayer
    {
        public string Author
        {
            get
            {
                return "Pierre";
            }
        }

        public string Name
        {
            get
            {
                return "xx_k1LL3r_xx";
            }
        }

        private List<Card> cards;
        private FoldResult lastResult;
        private RandomProvider rand;

        public void Deal(IEnumerable<Card> cards)
        {
            this.cards = cards.ToList();
        }

        public void Initialize(int playerCount, int position)
        {
            rand = new RandomProvider();
        }

        public Card PlayCard()
        {
            var x = rand.Random.Next(2);
            Card cardToPlay;

            if (x == 0)
                cardToPlay = cards.Min();
            else
                cardToPlay = cards.Max();

            if (lastResult != null)
            {
                cardToPlay = cards.Max();
                Card bestLastCard = lastResult.CardsPlayed.Max();

                foreach (Card c in cards)
                {
                    if (c.CompareTo(bestLastCard) > 0 && c.CompareTo(cardToPlay) < 0)
                    {
                        cardToPlay = c;
                    }
                }
            }

            cards.Remove(cardToPlay);

            return cardToPlay;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            lastResult = result;
        }
    }
}
