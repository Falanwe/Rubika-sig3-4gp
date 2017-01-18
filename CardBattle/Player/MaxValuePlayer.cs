using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;

namespace CardBattle.Player
{
    public class MaxValuePlayer //: IPlayer
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
                return "MaxValue";
            }
        }

        private List<Card> _cards;
        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
        }

        public void Initialize(int playerCount, int position)
        {
            // do nothing
        }

        public Card PlayCard()
        {
            var cardToPlay = _cards.Max();
            _cards.Remove(cardToPlay);
            return cardToPlay;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            // do nothing
        }
    }
}
