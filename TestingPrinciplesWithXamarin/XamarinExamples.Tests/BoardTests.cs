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
        public void LevelUp_CalledWithConstant_CallsCorrectMethodWithValue()
        {
            var player = Substitute.For<Player>(1, "first", "last");
            var board = new Board(player);

            board.LevelUp();

            // Test with the constant:
            player.Received().ApplyTokenFeatures(Arg.Is<string>(PlayerTokens.IntermediatePlayer));
        }
        
        [Fact]
        public void LevelUp_CalledWithRawString_CallsCorrectMethodWithValue()
        {
            var player = Substitute.For<Player>(1, "first", "last");
            var board = new Board(player);

            board.LevelUp();

            // Test with the string:
            player.Received().ApplyTokenFeatures(Arg.Is<string>("intermediate_player"));
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
        public void MakeMultipleMoves_Called_SetPropertyValues()
        {
            var player = new Player();
            var board = new Board(player);
            var first = 10;
            var final = 12;
            
            board.MakeMultipleMoves(first, final);

            board.FirstPosition.Should().Be(first);
            board.FinalPosition.Should().Be(final);
        }
    }
}