using FluentAssertions;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.Tests
{
    public class PlayerTests
    {
        private readonly Player player;
        
        public PlayerTests(Player player)
        {
            this.player = new Player(11);
        }

        [Fact]
        public void Constructor_CalledWithNoParams_SetsCorrectDefaultValues()
        {
            var player2 = new Player();

            player2.Seniority.Should().Be(1);
            player2.First.Should().Be("First");
            player2.Last.Should().Be("Last");
        }
        
        [Fact]
        public void Constructor_CalledWithParams_SetsCorrectValues()
        {
            var player2 = new Player(10, "Joe", "Smith");

            player2.Seniority.Should().Be(10);
            player2.First.Should().Be("Joe");
            player2.Last.Should().Be("Smith");
        }
        
        // Do not test this:
        [Fact]
        public void Seniority_Get_ReturnsValue()
        {
            player.Seniority.Should().Be(11);
        }
        
        // Do not test this:
        [Fact]
        public void Seniority_Set_ReturnsValue()
        {
            player.Seniority = 10;

            player.Seniority.Should().Be(10);
        }
        
        // Test custom get logic:
        [Fact]
        public void FullName_FirstNameSupplied_ReturnsCorrectValue()
        {
            var player2 = new Player(1, "Joe", "Smith");

            player2.FullName.Should().Be("Joe Smith");
        }
        
        [Fact]
        public void FullName_FirstNameNotSupplied_ReturnsCorrectValue()
        {
            player.FullName.Should().Be("Full Name Not Set");
        }
        
        [Fact]
        public void Last_Called_SetsToCorrectValue()
        {
            player.Last = "jones";

            player.Last.Should().Be("Jones");
        }
    }
}