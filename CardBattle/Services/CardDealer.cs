using CardBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Services
{
    public class CardDealer
    {
        private readonly ILogger _logger;
        private readonly RandomProvider _randomProvider;

        public CardDealer(ILogger logger, RandomProvider randomProvider)
        {
            _logger = logger;
            _randomProvider = randomProvider;
            Shuffle();
        }

        private List<Card> _availableCards = new List<Card>();

        public void Shuffle()
        {
            _logger.Log(LogLevel.Trace, "Deck shuffled!");

            _availableCards.Clear();
            foreach (Suits s in Enum.GetValues(typeof(Suits)))
            {
                foreach (Values v in Enum.GetValues(typeof(Values)))
                {
                    _availableCards.Add(new Card(v, s));
                }
            }
        }

        public IEnumerable<Card> Deal(int n)
        {
            for(var i = 0; i < n; i++)
            {
                yield return Deal();
            }
        }

        /// <summary>
        /// Deal a random card from available cards.
        /// </summary>
        /// <returns>A random card</returns>
        public Card Deal()
        {
            _logger.Log(LogLevel.Debug, "Dealt a card.");
            lock (_availableCards)
            {
                if (_availableCards.Count == 0)
                {
                    throw new InvalidOperationException("No card available!");
                }

                var index = _randomProvider.Random.Next(_availableCards.Count);
                var result = _availableCards[index];
                _availableCards.RemoveAt(index);

                return result;
            }
        }
    }
}
