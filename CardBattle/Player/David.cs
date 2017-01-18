using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;

namespace CardBattle.Player
{
    public class David : IPlayer
    {
        List<FoldResult> turns;
        int premier = 0;

        int test = 1;


        public string Author
        {
            get
            {
                return "DB";
            }
        }

        public string Name
        {
            get
            {
                return "Dave";
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

            for (var i = _cards.Count - 1; i > 2; i--)
            {
                var sorted = true;
                for (var j = 0; j < i; j++)
                {
                    sorted &= !SwapIfNeeded(_cards, j, j + 1);
                }
                if (sorted)
                {
                    break;
                }
            }
            test = test * -1;

            premier = premier + 1;
            var NbCarte = _cards.Count();
            
            
            if(test > 0)
            {
                 var cardToPlay = _cards[NbCarte-1];
                _cards.Remove(cardToPlay);
                return cardToPlay;

            }else
            {
                var cardToPlay = _cards[0];
                _cards.Remove(cardToPlay);
                return cardToPlay;
            }
               // Console.WriteLine("pute");
                
            
          



        }

        private static bool SwapIfNeeded<T>(List<T> list, int i, int j) where T : IComparable<T>
        {
            if (list[i].CompareTo(list[j]) > 0)
            {
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
                return true;
            }
            return false;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
          
        }
    }
}
