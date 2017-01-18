using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
using CardBattle.Services;

namespace CardBattle.Player
{
    class AnalyzePlayer : IPlayer
    {
        string IPlayer.Author
        {
            get
            {
                return "Benjamin";
            }
        }

        string IPlayer.Name
        {
            get
            {
                return "AnalyzePlayer";
            }
        }

        private ILogger _log;

        public AnalyzePlayer(ILogger log)
        {
            _log = log;
            _deck = new List<Card>();
            _prop = new Dictionary<Card, float>();
        }

        private List<Card> _deck;
        private List<Card> _cards;
        private Dictionary<Card, float> _prop;
        private int _id, _playerCount;

        void IPlayer.Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
            _deck.Clear();
            _prop.Clear();
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card c = new Card((Values)i, (Suits)j);
                    _deck.Add(c);
                }
            }

            foreach (Card c in cards)
            {
                _deck.Remove(c);
            }

            _cards.Sort();


            CalculateProb();
        }

        void IPlayer.Initialize(int playerCount, int position)
        {
            _id = position;
        }

        Card IPlayer.PlayCard()
        {
            Card selectCard = null;
            float prob = 0;
            foreach (var c in _cards)
            {
                float p = _prop[c];
                if (p < 0)
                    _log.Log(LogLevel.Error, c.ToString() + " " + p);
                if (prob > 0.5)
                {
                    if (p > 0.5 && p < prob)
                    {
                        prob = p;
                        selectCard = c;
                    }
                }
                else if (p >= prob)
                {
                    prob = p;
                    selectCard = c;
                }
            }
            _cards.Remove(selectCard);
            return selectCard;
        }

        void IPlayer.ReceiveFoldResult(FoldResult result)
        {
            for (int i = 0; i < _playerCount; i++)
            {
                if (i != _id)
                {
                    Card c = result.CardsPlayed.ToArray()[i];
                    _deck.Remove(c);
                }
            }

            CalculateProb();
        }

        void CalculateProb()
        {
            foreach (Card c in _cards)
            {
                float n = _deck.FindAll((x) =>
                {
                    if (c.Value > x.Value)
                        return true;
                    else if (c.Value == x.Value)
                        return c.Suit > x.Suit;
                    return false;
                }).Count;
                float prob = n / _deck.Count;
                if (!_prop.ContainsKey(c))
                    _prop.Add(c, prob);
                else
                    _prop[c] = prob;
                //_log.Log(LogLevel.Error, c.ToString() + " " + prob + " n:" + n);
            }
        }
    }
}
