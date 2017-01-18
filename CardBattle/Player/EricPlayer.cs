using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
using CardBattle.Enumerables;

namespace CardBattle.Player
{
    class EricPlayer : IPlayer
    {
        public string Author
        {
            get
            {
                return "JN";
            }
        }

        public string Name
        {
            get
            {
                return "IA";
            }
        }

        private List<Card> _cards;
        private int _turn = 0;
        private Card _lastCard, _cardOtherPlayer;

        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
            Sorts.BubleSort(_cards);
            _turn = _cards.Count / 2;
        }

        public void Initialize(int playerCount, int position)
        {

            
        }

        public Card PlayCard()
        {
            _lastCard = _cards[_turn];
            _cards.Remove(_lastCard);
            return _lastCard;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            if (_lastCard.CompareTo(result.CardsPlayed.Max()) < 0)
            {
                _turn = _cards.Count-1;
            }else
            {
                _turn = _cards.Count / 2;
                if (_turn > 1)
                    _turn--;
            }
        }
    }
}
