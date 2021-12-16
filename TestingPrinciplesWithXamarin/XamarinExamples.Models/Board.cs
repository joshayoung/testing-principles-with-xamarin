namespace XamarinExamples.Models
{
    public class Board
    {
        private readonly Player player;

        public int FirstPosition;
        public int FinalPosition;
        public IDeck Deck;
        
        public Board(Player player)
        {
            this.player = player;
        }

        public void MakeMultipleMoves(int firstLocation, int finalLocation)
        {
            this.FirstPosition = firstLocation;
            this.FinalPosition = finalLocation;
        }

        public void LevelUp()
        {
            this.player.ApplyTokenFeatures(PlayerTokens.IntermediatePlayer);
        }

        public int GetNumberOfMoves(int lives, int timeInPlay)
        {
            return lives * timeInPlay;
        }

        public void PowerBoost()
        {
            this.player.IncreasePlayerLives();
        }

        public void SetDeck(IDeck deck)
        {
            this.Deck = deck;
        }

        public void Sort()
        {
            this.Deck.Sort();
        }

        public int GetPlayerStrength()
        {
            return 100 * this.player.StrengthLevel();
        }
    }
}