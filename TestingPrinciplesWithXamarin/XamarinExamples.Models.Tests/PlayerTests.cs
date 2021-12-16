using FluentAssertions;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.Models.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Constructor_CalledWithNoParams_SetsCorrectDefaultValues()
        {
            var player = new Player();

            player.Seniority.Should().Be(1);
            player.First.Should().Be("First");
            // TODO: Last name is capitalized (which deviates a bit from my constructor test example):
            player.Last.Should().Be("LAST");
        }
        
        [Fact]
        public void Constructor_CalledWithParams_SetsCorrectValues()
        {
            var player = new Player(10, "Joe", "Smith");

            player.Seniority.Should().Be(10);
            player.First.Should().Be("Joe");
            // TODO: Last name is capitalized (which deviates a bit from my constructor test example):
            player.Last.Should().Be("SMITH");
        }
        
        // Do not test this:
        [Fact]
        public void GameDuration_Get_ReturnsValue()
        {
            var player = new Player(11);
            
            player.GameDuration.Should().Be(0);
        }
        
        // Do not test this:
        [Fact]
        public void GameDuration_Set_SetsCorrectValue()
        {
            var player = new Player();
            
            player.GameDuration = 10;

            player.GameDuration.Should().Be(10);
        }
        
        // Test custom get logic:
        [Fact]
        public void FullName_FirstNameSupplied_ReturnsCorrectValue()
        {
            var player = new Player(1, "Joe", "Smith");

            // TODO: Last name is capitalized (which deviates a bit from my full name test example):
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

        [Fact]
        public void GetStrengthLevelAndAssign_Called_SetsStrength()
        {
            var player = new Player();

            player.GetStrengthLevelAndAssign();

            player.Strength.Should().Be(11);
        }
    }
}