using FluentAssertions;
using NSubstitute;
using Xunit;

namespace XamarinExamples.Models.Tests
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
            var player = Substitute.For<Player>(1, "first", "last");
            player.StrengthLevel().Returns(11);
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

        // Using the concretion
        [Fact]
        public void AssignAndSortCards_Called_AssignsDeck()
        {
            var player = new Player();
            var board = new Board(player);
            var deck = Substitute.For<Deck>(10, false, false);

            board.SetDeck(deck);

            board.Deck.Should().BeEquivalentTo(deck);
        }


        [Fact]
        public void AssignAndSortCards_Called_SortsDeck()
        {
            var player = new Player();
            var board = new Board(player);
            var deck = Substitute.For<Deck>(10, false, false);

            board.SetDeck(deck);

            board.Deck.IsSorted().Should().BeTrue();
        }
        
        
        // Using the interface
        [Fact]
        public void AssignAndSortCards_CalledWithInterface_AssignsDeck()
        {
            var player = new Player();
            var board = new Board(player);
            var deck = Substitute.For<IDeck>();

            board.SetDeck(deck);

            board.Deck.Should().BeEquivalentTo(deck);
        }
    }
}