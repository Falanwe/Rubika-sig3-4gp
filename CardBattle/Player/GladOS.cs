using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;
using CardBattle.Enumerables;

namespace CardBattle.Player
{

    public class GladOS : IPlayer
    {
        public string Author
        {
            get
            {
                return "Aperture Science";
            }
        }

        public string Name
        {
            get
            {
                return "GladOS";
            }
        }

        private List<Card> _cards;
        private int[] _valuesArray;
        private int _maxIndex;
        private int _minIndex;
        private int _moy;

        public void Deal(IEnumerable<Card> cards)
        {
            _cards = cards.ToList();
            Sorts.QuickSort(_cards);
            _maxIndex = 0;
            _minIndex = 12;
            _valuesArray = new int[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _moy = 0;
        }

        public void Initialize(int playerCount, int position)
        {
        }

        public void Moyenne()
        {
            int total = 0;
            for(int i=0; i<_valuesArray.Length - 1; i++)
            {
                total += _valuesArray[i];
                _moy += i * _valuesArray[i];
            }

            _moy /= total;
        }

        public Card PlayCard()
        {
            Card cardToPlay = _cards.Max();

            foreach(Card c in _cards)
            {
                if((int)c.Value > _moy && c < cardToPlay)
                {
                    cardToPlay = c;
                }
            }

            _cards.Remove(cardToPlay);

            return cardToPlay;
        }

        public void ReceiveFoldResult(FoldResult result)
        {

            foreach(Card c in result.CardsPlayed)
            {
                _valuesArray[(int)c.Value]++;

                if ((int)c.Value >= _maxIndex)
                    _maxIndex = (int)c.Value;
                
                if ((int)c.Value <= _minIndex)
                    _minIndex = (int)c.Value;
            }

            Moyenne();
        }
    }
}