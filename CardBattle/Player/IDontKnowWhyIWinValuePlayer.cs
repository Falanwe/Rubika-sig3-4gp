using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardBattle.Models;

namespace CardBattle.Player {
    public class RandomValuePlayer : IPlayer {
        public string Author {
            get {
                return "fefelebg";
            }
        }

        public string Name {
            get {
                return "IDontKnowWhyIWin";
            }
        }

        private List<Card> _cards;
        public void Deal(IEnumerable<Card> cards) {
            _cards = cards.ToList();
        }

        public void Initialize(int playerCount, int position) {
            // do nothing
        }

        public Card PlayCard() {
            //foreach (var card in _cards) {
            //    Console.Write(card + " ; ");
            //}

            List<Card> secondhalf = _cards.Skip((int)(_cards.Count / 2)).Take(_cards.Count - (int)(_cards.Count / 2)).ToList(); //On ignore la moitié des cartes

            secondhalf.Sort();

            var cardToPlay = secondhalf.First(); //Et on retourne la plus nulle
            //Console.WriteLine(cardToPlay);

            _cards.Remove(cardToPlay);
            return cardToPlay; //Et on gagne x)
        }

        public void ReceiveFoldResult(FoldResult result) {
            // do nothing
        }
    }
}
