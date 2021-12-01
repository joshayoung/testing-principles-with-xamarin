using System.ComponentModel;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Extensions;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.ViewModels.Tests
{
    public class DieViewModelTests
    {
        [Fact]
        public void Model_PropertyChanges_ExpectVMPropertyChange()
        {
            var dieMock = Substitute.ForPartsOf<Die>(10);
            var dieViewModelMonitored = new DieViewModel(dieMock).Monitor();

            dieMock.Configure().PropertyChanged += 
                Raise.Event<PropertyChangedEventHandler>(this, new PropertyChangedEventArgs(nameof(Die.Rolled)));

            dieViewModelMonitored.Should()
                .Raise("PropertyChanged")
                .WithSender(dieViewModelMonitored.Subject)
                .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(DieViewModel.Rolled));
        }
        
        [Fact]
        public void Model_ValueChanges_ExpectVMValueChange()
        {
            var die = new Die(10);
            var dieViewModel = new DieViewModel(die);

            die.Rolled = true;

            dieViewModel.Rolled.Should().Be(true);
        }
    }
}