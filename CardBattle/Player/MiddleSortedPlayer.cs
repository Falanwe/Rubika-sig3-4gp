using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;

namespace CardBattle.Player
{
    class MiddleSortedPlayer : IPlayer
    {
        List<Card> _cards = new List<Card>();
        public string Author { get { return "Nico"; } }

        public string Name { get { return "Middle Sort AI"; } }

        public void Deal(IEnumerable<Card> cards)
        {
            _cards.Clear();
            foreach(var card in cards.OrderBy(_ => _).ToList())
            {
                _cards.Insert(_cards.Count / 2, card);
            }
        }

        public void Initialize(int playerCount, int position)
        {
            //nothing to do
        }

        public Card PlayCard()
        {
            Card c = _cards[0];
            _cards.RemoveAt(0);
            return c;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            //nothing to do
        }
    }
}
