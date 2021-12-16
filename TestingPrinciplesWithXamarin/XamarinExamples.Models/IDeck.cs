namespace XamarinExamples.Models
{
    public interface IDeck
    {
        void AddCards(int numberCards);

        int GetNumberOfCards();

        bool IsSorted();

        void Sort();
    }
}