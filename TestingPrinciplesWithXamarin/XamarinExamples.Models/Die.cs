using System.ComponentModel;

namespace XamarinExamples.Models
{
    public class Die : INotifyPropertyChanged
    {
        // The property change is being handled automatically by Fody
        public bool Rolled { get; set; }

        public void Roll()
        {
            // this method would perform logic followed by triggering a property change
            // ...
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DieRolled"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}