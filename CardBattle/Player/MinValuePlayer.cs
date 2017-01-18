using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;

namespace CardBattle.Player
{
    public class MinValuePlayer : IPlayer
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
                return "MinValue";
            }
        }

        private List<Card> _cards;
        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.OrderBy(c => c).ToList();
            _turn = 0;
        }

        public void Initialize(int playerCount, int position)
        {
            _turn = 0;
        }

        private int _turn = 0;
        public Card PlayCard()
        {
            var result = _cards[_turn];
            _turn++;
            return result;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
        }
    }
}
