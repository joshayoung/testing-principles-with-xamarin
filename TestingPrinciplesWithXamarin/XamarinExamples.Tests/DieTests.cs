using System.ComponentModel;
using FluentAssertions;
using FluentAssertions.Equivalency.Steps;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.Tests
{
    public class DieTests
    {
        [Fact]
        public void Constructor_DefaultValues_ExpectCorrectAssignment()
        {
            var die = new Die();
            
            die.NumberOfSides.Should().Be(1);
        }
        
        [Fact]
        public void Constructor_Called_ExpectCorrectAssignment()
        {
            var die = new Die(10);
            
            die.NumberOfSides.Should().Be(10);
        }
        
        // Just testing Fody, do not test
        [Fact]
        public void Rolled_Changes_ExpectPropertyChangedEvent()
        {
            var dieMonitored = new Die(10).Monitor();
        
            dieMonitored.Subject.Rolled = true;
        
            dieMonitored.Should().RaisePropertyChangeFor(d => d.Rolled);
        }
        
        [Fact]
        public void Roll_Called_TriggersCustomPropertyChange()
        {
            var dieMonitored = new Die(10).Monitor();
        
            dieMonitored.Subject.Roll();
        
            dieMonitored.Should().Raise("PropertyChanged")
                .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == "DieRolled");
        }
    }
}