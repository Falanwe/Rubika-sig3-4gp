using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
using CardBattle.Enumerables;
using CardBattle.Services;

namespace CardBattle.Player
{
    class YG_CounterIAPlayer //:  IPlayer
    {
        public string Author
        {
            get
            {
                return "Yoan Garnier";
            }
        }

        public string Name
        {
            get
            {
                return "SafetyPlayer";
            }
        }

        private int _cardCountOnBegin;
        private int _playerCount;
        private Random _rand;
        private List<Card> _win;
        private List<Card> _hand;
        private List<Card> _playedCards;
        public void Deal(IEnumerable<Card> cards)
        {
            _hand = cards.ToList();
            _cardCountOnBegin = _hand.Count;
            Sorts.QuickSort<Card>(_hand);
            _hand.Reverse();
        }

        public void Initialize(int playerCount, int position)
        {
            _playerCount = playerCount;
            _win = new List<Card>();
            _playedCards = new List<Card>();
            RandomProvider tmp = new RandomProvider();
            _rand = tmp.Random;
        }

        private int nbCardBetterThan(Card card)
        {
            return ((int)Values.Ace - (int)card.Value) * 4 + ((int)Suits.Spades - (int)card.Suit);
        }

        private int nbCardPlayedBetterThan(Card card)
        {
            var res = 0;
            foreach(var element in _playedCards.Concat(_hand).Concat(_win))
            {
                if(element > card)
                {
                    res++;
                }
            }
            return res;
        }

        public Card PlayCard()
        {
            Card res;
            if(_hand.Count > 0)
            {
                if (nbCardPlayedBetterThan(_hand.First()) == nbCardBetterThan(_hand.First()))
                {
                    _win.Add(_hand.First());
                    _hand.Remove(_hand.First());
                    return PlayCard();
                }
                else
                {
                    res = _hand.Last();
                    _hand.Remove(res);
                }
            }
            else
            {
                if (_win.Count > 0)
                {
                    res = _win.Last();
                    _win.Remove(res);
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            return res;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            foreach(var element in result.CardsPlayed)
            {
                _playedCards.Add(element);
            }
        }
    }
}
