namespace XamarinExamples.Models
{
    public class Board
    {
        private readonly Player player;

        public int FirstPostion;
        public int FinalPostion;
        
        public Board(Player player)
        {
            this.player = player;
        }

        public void MakeAMove(int location)
        {
            this.FinalPostion = location;
        }

        public void MakeMultipleMoves(int firstLocation, int finalLocation)
        {
            this.FirstPostion = firstLocation;
            this.FinalPostion = finalLocation;
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