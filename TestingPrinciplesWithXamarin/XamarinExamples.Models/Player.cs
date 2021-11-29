using System;

namespace XamarinExamples.Models
{
    public class Player
    {
        public int Seniority { get; set; }
        
        public string First { get; set; }

        private string last;
        public string Last
        {
            get => last;
            set
            {
                this.last = value.ToUpperInvariant();
            }
        }

        public string FullName
        {
            get
            {
                if (this.First != "First")
                {
                    return $"{this.First} {this.Last}";
                }

                return "Full Name Not Set";
            }
        }
        
        public Player(int seniority = 1, string first = "First", string last = "Last")
        {
            this.Seniority = seniority;
            this.First = first;
            this.Last = last;
        }
        
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