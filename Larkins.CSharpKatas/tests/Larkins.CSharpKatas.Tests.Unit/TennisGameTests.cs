using FluentAssertions;
using Xunit;

namespace Larkins.CSharpKatas.Tests.Unit
{
    public class TennisGameTests
    {
        [Theory]
        [InlineData(0, 3, "Love-Forty")]
        [InlineData(1, 2, "Fifteen-Thirty")]
        [InlineData(2, 1, "Thirty-Fifteen")]
        [InlineData(3, 0, "Forty-Love")]
        [InlineData(2, 2, "Thirty-All")]
        [InlineData(3, 3, "Deuce")]
        [InlineData(4, 3, "Advantage Player 1")]
        [InlineData(3, 4, "Advantage Player 2")]
        [InlineData(4, 0, "Player 1 Wins")]
        [InlineData(5, 3, "Player 1 Wins")]
        [InlineData(6, 8, "Player 2 Wins")]
        public void Tennis_game_score_for_different_point_combinations(
            int playerOneScore,
            int playerTwoScore,
            string expected)
        {
            // Arrange
            var sut = new TennisGame
            {
                PlayerOneScore = playerOneScore,
                PlayerTwoScore = playerTwoScore
            };

            // Act
            var result = sut.GetScore();

            // Assert
            result.Should().Be(expected);
        }
    }
}
