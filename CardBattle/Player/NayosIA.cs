using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
 
namespace CardBattle.Player
{
    public class CrazedIA : IPlayer
    {
        public string Author
        {
            get
            {
                return "Marc";
            }
        }
 
        public string Name
        {
            get
            {
                return "CrazedOutFool";
            }
        }
 
 
        private List<Card> _cards;
        private List<Card> _usedCards;
        private List<Card> _remainingCards;
        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
        }
       
        public void Initialize(int playerCount, int position)
        {
            _usedCards = new List<Card>();
            _remainingCards = new List<Card>();
            foreach (Suits s in Enum.GetValues(typeof(Suits)))
            {
                foreach (Values v in Enum.GetValues(typeof(Values)))
                {
                    _remainingCards.Add(new Card(v, s));
                }
            }
        }
       
        public Card PlayCard()
        {
 
            if(_cards.Max() == _remainingCards.Max())
            {
                var cardToPlay = _cards.Max();
                _cards.Remove(cardToPlay);
                return cardToPlay;
            }
            else
            {
                var cardToPlay = _cards.Min();
                _cards.Remove(cardToPlay);
                return cardToPlay;
            }
        }
 
        public void ReceiveFoldResult(FoldResult result)
        {
            foreach(Card c in result.CardsPlayed)
            {
                _remainingCards.Remove(c);
            }
        }
    }
}