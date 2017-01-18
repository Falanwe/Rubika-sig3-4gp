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
    class YG_CounterIAPlayer : IPlayer
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

        private int _player;
        private int _betterCardPlayed;
        private int _lessCardPlayed;
        private List<Card> _better;
        private List<Card> _cards;
        private List<Card> _cardPlayed;
        public void Deal(IEnumerable<Card> cards)
        {
            _betterCardPlayed = 0;
            _cards = cards.ToList();
            _cards.Sort();
            _cards.Reverse();
        }

        private int CardValue(Card card){
            return (int)card.Value * 4 + (int)card.Suit + 1;
        }

        private int CardBetterThan(Card card){
            Card tmp = new Card(Values.Ace, Suits.Spades);
            return CardValue(tmp) - CardValue(card);
        }

        public void Initialize(int playerCount, int position)
        {
            _player = playerCount;
            _better = new List<Card>();
            _cardPlayed = new List<Card>();
        }

        private void updateCardPlayed()
        {
            if (_cards.Count > 0)
            {
                List<Card> tmp = new List<Card>(_cardPlayed);
                foreach (var element in tmp)
                {
                    if (element > _cards.First())
                    {
                        _betterCardPlayed++;
                        _cardPlayed.Remove(element);
                    }
                    else if (element < _cards.Last())
                    {
                        _lessCardPlayed++;
                        _cardPlayed.Remove(element);
                    }
                }
            }
        }

        public Card PlayCard()
        {
            if (_cards.Count > 0)
            {
                if (CardBetterThan(_cards.First()) == _betterCardPlayed)
                {
                    _better.Add(_cards.First());
                    _cards.Remove(_cards.First());
                    updateCardPlayed();
                    return PlayCard();
                }
                else
                {
                    var ratioMin = _lessCardPlayed / CardValue(_cards.Last());
                    var ratioMax = _betterCardPlayed / CardBetterThan(_cards.First());
                    Card play;
                    if(ratioMin * _player < ratioMax)
                    {
                        play = _cards.First();
                    }
                    else
                    {
                        play = _cards.Last();
                    }
                    _cards.Remove(play);
                    return play;
                }
            }
            else
            {
                var play = _better.Last();
                _better.Remove(play);
                return play;
            }
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            foreach(var element in result.CardsPlayed){
                if (_cards.Count > 0)
                {
                    if (element > _cards.First())
                    {
                        _betterCardPlayed++;
                    }
                    else if (element < _cards.Last())
                    {
                        _lessCardPlayed++;
                    }
                    else
                    {
                        _cardPlayed.Add(element);
                    }
                }
            }
        }
    }
}
