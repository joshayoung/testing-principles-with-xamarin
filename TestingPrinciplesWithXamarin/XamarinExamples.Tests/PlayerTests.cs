using FluentAssertions;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Constructor_CalledWithNoParams_SetsCorrectDefaultValues()
        {
            var player2 = new Player();

            player2.Seniority.Should().Be(1);
            player2.First.Should().Be("First");
        }
        
        [Fact]
        public void Constructor_CalledWithParams_SetsCorrectValues()
        {
            var player2 = new Player(10, "Joe", "Smith");

            player2.Seniority.Should().Be(10);
            player2.First.Should().Be("Joe");
            player2.Last.Should().Be("SMITH");
        }
        
        // Do not test this:
        [Fact]
        public void Seniority_Get_ReturnsValue()
        {
            var player = new Player(11);
            
            player.Seniority.Should().Be(11);
        }
        
        // Do not test this:
        [Fact]
        public void Seniority_Set_ReturnsValue()
        {
            var player = new Player();
            
            player.Seniority = 10;

            player.Seniority.Should().Be(10);
        }
        
        // Test custom get logic:
        [Fact]
        public void FullName_FirstNameSupplied_ReturnsCorrectValue()
        {
            var player = new Player(1, "Joe", "Smith");

            player.FullName.Should().Be("Joe SMITH");
        }
        
        [Fact]
        public void FullName_FirstNameNotSupplied_ReturnsCorrectValue()
        {
            var player = new Player();
            
            player.FullName.Should().Be("Full Name Not Set");
        }
        
        // Test custom set logic:
        [Fact]
        public void Last_Called_SetsToCorrectValue()
        {
            var player = new Player();
            
            player.Last = "jones";

            player.Last.Should().Be("JONES");
        }
    }
}