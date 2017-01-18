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
        }

        public Card PlayCard()
        {
            if(this.CardsInHand.Count == 1)
            {
                Card card = this.CardsInHand[0];
                this.CardsInHand.Remove(card);
                return card;
            }
            else
            {
                Card card;

                this.CardsInHand.Sort((a,b)=> b.CompareTo(a));
                this.CardDeck.Sort((a, b) => b.CompareTo(a));

                int deltaValue = (int)(this.CardDeck.First().Value) - (int)(this.CardsInHand.First().Value);

                if(deltaValue >= 5)
                {
                    card = this.CardsInHand.Last();
                }
                else if(deltaValue >= 0)
                {
                    int middle = this.CardsInHand.Count / 2;
                    card = this.CardsInHand[middle];
                }
                else
                {
                    card = this.CardsInHand.First();
                }

                this.CardsInHand.Remove(card);
                return card;
            }
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            foreach(Card playedCard in result.CardsPlayed)
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
