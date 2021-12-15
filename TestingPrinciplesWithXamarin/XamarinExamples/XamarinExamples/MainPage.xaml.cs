using Xamarin.Forms;
using XamarinExamples.Models;

namespace XamarinExamples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var die = new Die(10);
            var die2 = new Die();
        }
    }
}