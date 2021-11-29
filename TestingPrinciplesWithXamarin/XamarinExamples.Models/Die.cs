using System.ComponentModel;

namespace XamarinExamples.Models
{
    public class Die : INotifyPropertyChanged
    {
        // The property change is being handled automatically by Fody
        public bool Rolled { get; set; }

        public void Roll()
        {
            //... other logic
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DieRolled"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}