namespace XamarinExamples.Models
{
    public class Deck : IDeck
    {
        private bool Sorted { get; set; }
        private readonly bool CompleteDeck;
        
        public int Number { get; set; }
        
        public Deck(int number, bool sorted, bool completeDeck)
        {
            this.Number = number;
            this.Sorted = sorted;
            this.CompleteDeck = completeDeck;
        }

        public void AddCards(int numberCards)
        {
            this.Number += numberCards;
        }

        public int GetNumberOfCards() => this.Number;

        public void Sort()
        {
            this.Sorted = true;
        }

        public bool IsSorted()
        {
            return this.Sorted;
        }
    }
}