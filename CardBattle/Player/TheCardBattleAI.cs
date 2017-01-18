using CardBattle.Models;
using CardBattle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Player
{
    class TheCardBattleAI : IPlayer
    {
        private ILogger Logger { get; set; }
        private RandomProvider RandomProvider { get; set; }

        private int PlayerCount { get; set; }
        private int Position { get; set; }
        private int DealtCardsCount { get; set; }

        private List<Card> CardDeck { get; set; }
        private List<Card> DealtCards { get; set; }
        private List<Card> CardsInHand { get; set; }

        public string Name { get { return "TheCardBattleAI"; } }
        public string Author { get { return "Patrick"; } }

        public TheCardBattleAI(ILogger logger, RandomProvider random)
        {
            this.Logger = logger;
            this.RandomProvider = random;
        }

        public void Initialize(int playerCount, int position)
        {
            this.PlayerCount = playerCount;
            this.Position = position;

            this.CardDeck = new List<Card>();
            this.DealtCards = new List<Card>();
            this.CardsInHand = new List<Card>();
        }

        public void Deal(IEnumerable<Card> cards)
        {
            this.CardDeck.Clear();
            this.DealtCards.Clear();
            this.CardsInHand.Clear();

            this.DealtCardsCount = cards.Count();

            foreach (Suits s in Enum.GetValues(typeof(Suits)))
            {
                foreach (Values v in Enum.GetValues(typeof(Values)))
                {
                    this.CardDeck.Add(new Card(v, s));
                }
            }

            foreach (Card dealtCard in cards)
            {
                this.CardDeck.Remove(dealtCard);
                this.DealtCards.Add(dealtCard);
                this.CardsInHand.Add(dealtCard);
            }

            this.CardDeck.Sort((a, b) => b.CompareTo(a));
            this.CardDeck.Sort((a, b) => b.CompareTo(a));
        }

        public Card PlayCard()
        {
            // 1 CARD LEFT
            if (this.CardsInHand.Count == 1)
            {
                Card card = this.CardsInHand[0];
                this.CardsInHand.Remove(card);
                return card;
            }
            // 2 CARDS LEFT
            else if(this.CardsInHand.Count == 2)
            {
                Card card = this.CardsInHand.Max();
                this.CardsInHand.Remove(card);
                return card;
            }
            // ELSE
            else
            {
                Card card = null;

                int deltaValue = (int)(this.CardDeck.First().Value) - (int)(this.CardsInHand.First().Value);

                /*if (deltaValue >= 5)
                {
                    card = this.CardsInHand.Last();
                }
                else */if (deltaValue >= 0)
                {
                    //int middle = this.CardsInHand.Count / 2;
                    int middle = this.RandomProvider.Random.Next(1, this.CardsInHand.Count - 1);
                    card = this.CardsInHand[middle];
                }
                else
                {
                    card = this.CardsInHand.First();
                }

                //*********************
                if (card == null)
                {
                    card = this.CardsInHand.Last();
                }

                this.CardsInHand.Remove(card);
                return card;
            }
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            foreach (Card playedCard in result.CardsPlayed)
            {
                if (!this.DealtCards.Contains(playedCard))
                {
                    this.CardDeck.Remove(playedCard);
                    this.DealtCards.Add(playedCard);
                }
            }
        }
    }
}
