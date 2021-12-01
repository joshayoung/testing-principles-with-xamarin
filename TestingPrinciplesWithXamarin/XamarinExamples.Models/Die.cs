using System.ComponentModel;

namespace XamarinExamples.Models
{
    public class Die : INotifyPropertyChanged
    {
        public readonly int NumberOfSides;
        
        public Die(int numberOfSides = 1)
        {
            this.NumberOfSides = numberOfSides;
        }
        
        // The property change is being handled automatically by Fody
        public bool Rolled { get; set; }

        public void Roll()
        {
            // this method would perform logic followed by triggering a property change
            // ...
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DieRolled"));
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}