using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
using CardBattle.Services;
using CardBattle.Enumerables;

namespace CardBattle.Player
{
    public class MemoryPlayer : IPlayer
    {
        public MemoryPlayer (ILogger logger)
        {
            _logger = logger;
        }

        public string Author
        {
            get
            {
                return "Keryann MALACCHINA";
            }
        }

        public string Name
        {
            get
            {
                return "Kiki";
            }
        }

        public int Position
        {
            get
            {
                return _position;
            }
        }

        private int _position;

        private ILogger _logger;
        private RandomProvider _randomProvider;

        private List<Card> _cards;
        private List<Card> _playedCards = new List<Card>();
        private int[] _valuesPlayed = new int[13];

        private int[] _wins = new int[0];

        private Card _previousWinningCard;

        public void Deal(IEnumerable<Card> cards)
        {
            _playedCards.Clear();
            _valuesPlayed = new int[13];

            for (int i = 0; i < 13; i++)
                _valuesPlayed[i] = 0;

            _cards = cards.ToList();
            _cards = Sorts.BubleSort(_cards);

            //for (int i = 0; i < _cards.Count; i++)
               // _logger.Log(LogLevel.Warning, _cards[i].Value + " " + _cards[i].Suit);
        }

        public void Initialize(int playerCount, int position)
        {
            _position = position;
            _wins = new int[playerCount];
            _randomProvider = new RandomProvider();
        }

        public Card PlayCard()
        {
            var cardToPlay = _cards[0];

            int emptyValueInARow = 0;
            int index = 0;

            for (int i = 0; i < 13; i++)
            {
                if (_valuesPlayed[i] < 2)
                    emptyValueInARow++;

                if (emptyValueInARow == 4)
                {
                    index = i;
                    break;
                }
            }

            if (index >= _cards.Count)
                index = _cards.Count - 1;

            cardToPlay = _cards[index];
            _cards.Remove(cardToPlay);

            return cardToPlay;
        }       

        public void ReceiveFoldResult(FoldResult result)
        {
            _wins[result.Winner]++;
            _previousWinningCard = result.CardsPlayed.First();

            foreach (var card in result.CardsPlayed)
            {
                _playedCards.Add(card);
                _valuesPlayed[(int)card.Value]++;

                if (_previousWinningCard.CompareTo(card) < 0)
                    _previousWinningCard = card;
            }

            //_logger.Log(LogLevel.Warning, "Wining Card : " + _previousWinningCard.Value + " of " + _previousWinningCard.Suit);
        }
    }
}
