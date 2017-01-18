using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
using CardBattle.Services;
using System.Reflection;

namespace CardBattle.Player
{
    class BestPlayer : IPlayer
    {
        public string Author
        {
            get
            {
                return "Benjamin";
            }
        }

        public string Name
        {
            get
            {
                return "BestPlayer";
            }
        }


        private int _playerCount = 0;
        private int _position = 0;
        private int _power = 0;
        private int _initialPower = 0;
        private int _averagePower;
        private int _variance;
        private int _maxPotentialPower;
        private int _enemyPowerUsed;
        private float _ecartType;
        private List<Card> _cards;
        private ILogger _logger;

        public BestPlayer(ILogger logger)
        {
            _logger = logger;
        }

        public void Initialize(int playerCount, int position)
        {
            _playerCount = playerCount;
            _position = position;
        }

        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();

            _maxPotentialPower = _cards.Count * 14;

            foreach (var c in cards)
            {
                _power += (int)c.Value + 2;
            }
            foreach (var c in cards)
            {
                _variance += (_averagePower - (int)c.Value + 2) * (_averagePower - (int)c.Value + 2);
            }

            _initialPower = _power;
            _averagePower = _power / _cards.Count;
            _variance /= _cards.Count;
            _ecartType = (float)Math.Sqrt(_variance);


            LogLevel level = LogLevel.Info;

            _logger.Log(level, " Power : " + _power);
            _logger.Log(level, " MaxPower : " + _maxPotentialPower);
            _logger.Log(level, " AveragePower : " + _averagePower);
            _logger.Log(level, " variance : " + _variance);
            _logger.Log(level, " ecart-type : " + _ecartType);
            _cards.Sort();
            _cards.Reverse();
            foreach (var c in _cards)
            {
                _logger.Log(level, c.ToString());
            }
            _logger.Log(level, "---------------");


        }

        public Card PlayCard()
        {
            Card card;

            if (_initialPower < _maxPotentialPower * 0.0)
                card = _cards.Min();
            else
                if (_power > _enemyPowerUsed)
            {
                card = _cards.Find((c) => (int)c.Value + 2 < 7);
                if (card == null)
                    card = _cards.Min();
            }
            else
                card = _cards.Max();



            _cards.Remove(card);
            _power -= (int)card.Value + 2;
            return card;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            _enemyPowerUsed += (int)result.CardsPlayed.ToArray()[0].Value + 2;
        }
    }
}
