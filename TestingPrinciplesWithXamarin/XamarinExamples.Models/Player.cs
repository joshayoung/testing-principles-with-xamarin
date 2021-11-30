namespace XamarinExamples.Models
{
    public class Player
    {
        public int Seniority { get; set; }
        
        public int Strength { get; set; }
        
        public int GameDuration { get; set; }
        
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
            // ...
        }

        // Simple method, for the sake of example
        public int StrengthLevel()
        {
            return this.DeriveStrengthLevel();
        }

        // Yes, this method is a code-smell, just here as an example
        public void GetStrengthLevelAndAssign()
        {
            // ... more logic
            this.AssignStrengthLevel();
            // ... more logic
        }

        private void AssignStrengthLevel()
        {
            this.Strength = this.StrengthLevel();
        }

        // Simple method, for the sake of example
        private int DeriveStrengthLevel()
        {
            return 11;
        }
    }
}