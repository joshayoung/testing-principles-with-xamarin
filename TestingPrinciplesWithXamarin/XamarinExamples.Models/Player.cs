using System;

namespace XamarinExamples.Models
{
    public class Player
    {
        private int Seniority { get; set; }
        
        public void IncreasePlayerLives()
        {
            Console.WriteLine("Example");
        }

        // This would be a much more complicated method, but simple for the sake of example
        public int StrengthLevel()
        {
            this.DeriveStrenthLevel();
        }

        // This would be a much more complicated method, but simple for the sake of example
        private int DeriveStrenthLevel()
        {
            return 11;
        }
    }
}