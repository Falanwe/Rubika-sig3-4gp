using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Models
{
    public class Card : IEquatable<Card>, IComparable<Card>, IComparable
    {
        public Card(Values value, Suits suit)
        {
            _value = value;
            _suit = suit;
        }

        private readonly Suits _suit;
        public Suits Suit
        {
            get
            {
                return _suit;
            }
        }

        private readonly Values _value;
        public Values Value
        {
            get
            {
                return _value;
            }
        }

        public override string ToString()
        {
            //return $"{Value} of {Suit}";
            return String.Format("{0} of {1}", Value, Suit);
        }

        public override bool Equals(object obj)
        {
            var card2 = obj as Card;
            return Equals(card2);
        }

        public bool Equals(Card other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else
            {
                return Suit == other.Suit && Value == other.Value;
            }
        }

        public static bool operator ==(Card card1, Card card2)
        {
            if (ReferenceEquals(card1, null))
            {
                return ReferenceEquals(card2, null);
            }
            else
            {
                return card1.Equals(card2);
            }
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

        private const int PRIME = 15485867;

        public override int GetHashCode()
        {
            return Value.GetHashCode() + PRIME * Suit.GetHashCode();
        }

        public int CompareTo(Card other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            var valueCompare = Value.CompareTo(other.Value);

            if (valueCompare != 0)
            {
                return valueCompare;
            }
            else
            {
                return Suit.CompareTo(other.Suit);
            }
        }

        public int CompareTo(object obj)
        {
            var card = obj as Card;

            if (obj != null && ReferenceEquals(card, null))
            {
                throw new ArgumentException("obj is not a card.");
            }

            return CompareTo(card);
        }

        public static bool operator >(Card card1, Card card2)
        {
            if (card1 == null)
            {
                return false;
            }
            else
            {
                return card1.CompareTo(card2) > 0;
            }
        }

        public static bool operator <(Card card1, Card card2)
        {
            return card2 > card1;
        }

        public static bool operator >=(Card card1, Card card2)
        {
            return !(card1 < card2);
        }

        public static bool operator <=(Card card1, Card card2)
        {
            return !(card1 > card2);
        }
    }
}
