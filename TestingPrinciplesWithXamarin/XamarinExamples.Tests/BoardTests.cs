using FluentAssertions;
using NSubstitute;
using XamarinExamples.Models;
using Xunit;

namespace XamarinExamples.Tests
{
    public class BoardTests
    {
        [Fact]
        public void GetNumberOfMoves_Called_ReturnsCorrectValue()
        {
            var player = new Player();
            var board = new Board(player);
            
            var result = board.GetNumberOfMoves(2, 20);

            result.Should().Be(40);
        }

        [Fact]
        public void PowerBoost_Called_CallsCorrectMethod()
        {
            var player = Substitute.For<Player>(1, "first", "last");
            var board = new Board(player);

            board.PowerBoost();

            player.Received().IncreasePlayerLives();
        }
        
        [Fact]
        public void GetPlayerStrength_Called_ReturnsTheCorrectValue()
        {
            var player = new Player();
            var board = new Board(player);

            var result = board.GetPlayerStrength();

            result.Should().Be(1100);
        }
        
        [Fact]
        public void MakeAMove_Called_SetPropertyValue()
        {
            var player = new Player();
            var board = new Board(player);
            var position = 10;
            
            board.MakeAMove(position);

            board.FirstPostion.Should().Be(position);
        }
        
        [Fact]
        public void MakeMultipleMoves_Called_SetPropertyValues()
        {
            var player = new Player();
            var board = new Board(player);
            var first = 10;
            var final = 12;
            
            board.MakeMultipleMoves(first, final);

            board.FirstPostion.Should().Be(first);
            board.FinalPostion.Should().Be(final);
        }
    }
}