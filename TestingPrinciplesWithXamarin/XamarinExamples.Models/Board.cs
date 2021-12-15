namespace XamarinExamples.Models
{
    public class Board
    {
        private readonly Player player;

        public int FirstPosition;
        public int FinalPosition;
        
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

        public int GetPlayerStrength()
        {
            return 100 * this.player.StrengthLevel();
        }
    }
}