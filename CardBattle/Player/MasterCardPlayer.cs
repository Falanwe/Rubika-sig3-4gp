using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;

namespace CardBattle.Player
{
    public class MasterCardPlayer : IPlayer
    {
        public string Author
        {
            get
            {
                return "Yann Desmarais";
            }
        }

        public string Name
        {
            get
            {
                return "MasterCard";
            }
        }

        private List<Card> _cards;
        private List<Card> _deck = new List<Card>();
        private int _currentTurn = 0;

        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
            _deck.Clear();
            foreach (Suits s in Enum.GetValues(typeof(Suits)))
            {
                foreach (Values v in Enum.GetValues(typeof(Values)))
                {
                    _deck.Add(new Card(v, s));
                }
            }
        }

        public void Initialize(int playerCount, int position)
        {

        }

        public Card PlayCard()
        {
            var cardToPlay = _cards.Max();
            if(_currentTurn > 1)
            {
                if(_cards.Max().CompareTo(_deck.Max()) <= 0)
                {
                    cardToPlay = _cards.Min();
                }
            } else
            {
                cardToPlay = _cards.Min();
            }
            _cards.Remove(cardToPlay);
            return cardToPlay;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            _currentTurn = result.Turn;
            foreach(Card usedCard in result.CardsPlayed)
            {
                _deck.Remove(usedCard);
            }
        }
    }
}
