using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.Tests
{
    public class DieTests
    {
        // Just testing Fody, do not test
        [Fact]
        public void Rolled_Changes_ExpectPropertyChangedEvent()
        {
            var dieMonitored = new Die().Monitor();
        
            dieMonitored.Subject.Rolled = true;
        
            dieMonitored.Should().RaisePropertyChangeFor(d => d.Rolled);
        }
        
        [Fact]
        public void Roll_Called_TriggersCustomPropertyChange()
        {
            var dieMonitored = new Die().Monitor();
        
            dieMonitored.Subject.Roll();
        
            dieMonitored.Should().Raise("PropertyChanged").WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == "DieRolled");
        }
    }
}