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
        private Random _random = new Random();

        private List<Card> _availableCards = new List<Card>();

        public void Shuffle()
        {
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

        public Card Deal()
        {
            lock (_availableCards)
            {
                if (_availableCards.Count == 0)
                {
                    throw new InvalidOperationException("No card available!");
                }

                var index = _random.Next(_availableCards.Count);
                var result = _availableCards[index];
                _availableCards.RemoveAt(index);

                return result;
            }
        }
    }
}
