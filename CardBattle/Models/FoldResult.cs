using System.Collections.Generic;

namespace CardBattle.Models
{
    public class FoldResult
    {
        public FoldResult(IEnumerable<Card> cards, int winnerIndex, int turn)
        {
            CardsPlayed = cards;
            Winner = winnerIndex;
            Turn = turn;
        }

        public IEnumerable<Card> CardsPlayed { get; private set; }
        public int Winner { get; private set; }
        public int Turn { get; private set; }
    }
}