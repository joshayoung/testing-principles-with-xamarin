using System.ComponentModel;
using XamarinExamples.Models;

namespace XamarinExamples.ViewModels
{
    public class DieViewModel : INotifyPropertyChanged
    {
        private readonly Die die;
        
        public DieViewModel(Die die)
        {
            this.die = die;
            
            // Automatic properties change trigger for like-named properties in view model:
            die.PropertyChanged += (sender, args) => this.PropertyChanged?.Invoke(this, args);
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Rolled
        {
            get => this.die.Rolled;
            set => this.die.Rolled = value;
        }
    }
}