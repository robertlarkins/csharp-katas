using System;
using System.Collections.Generic;

namespace Larkins.CSharpKatas
{
    /// <summary>
    /// Wraps a Tennis Game.
    /// It is based on this Kata:
    /// https://josepaumard.github.io/katas/intermediate/tennis-kata.html.
    /// https://codingdojo.org/kata/Tennis/.
    /// </summary>
    public class TennisGame
    {
        private readonly Dictionary<int, string> scoreLookup = new()
        {
            { 0, "Love" },
            { 1, "Fifteen" },
            { 2, "Thirty" },
            { 3, "Forty" }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="TennisGame"/> class.
        /// </summary>
        /// <param name="playerOneStartScore">The player one start score.</param>
        /// <param name="playerTwoStartScore">The player two start score.</param>
        public TennisGame(int playerOneStartScore = 0, int playerTwoStartScore = 0)
        {
            PlayerOneScore = playerOneStartScore;
            PlayerTwoScore = playerTwoStartScore;
        }

        /// <summary>
        /// Gets player one's score.
        /// </summary>
        public int PlayerOneScore { get; private set; }

        /// <summary>
        /// Gets the player two's score.
        /// </summary>
        public int PlayerTwoScore { get; private set; }

        /// <summary>
        /// Gets the score difference between the two players.
        /// </summary>
        public int ScoreDifference => Math.Abs(PlayerOneScore - PlayerTwoScore);

        /// <summary>
        /// Gets a value indicating whether the game tied.
        /// </summary>
        public bool IsGameTied => ScoreDifference == 0;

        /// <summary>
        /// Increases the player ones score.
        /// </summary>
        public void IncreasePlayerOnesScore() => PlayerOneScore++;

        /// <summary>
        /// Increases the player twos score.
        /// </summary>
        public void IncreasePlayerTwosScore() => PlayerTwoScore++;

        /// <summary>
        /// Gets the current score of the Tennis game.
        /// </summary>
        /// <returns>The score.</returns>
        public string GetScore()
        {
            if (IsGameTied)
            {
                if (PlayerOneScore < 3)
                {
                    return $"{scoreLookup[PlayerOneScore]}-All";
                }

                return "Deuce";
            }

            if (PlayerOneScore <= 3 && PlayerTwoScore <= 3)
            {
                return $"{scoreLookup[PlayerOneScore]}-{scoreLookup[PlayerTwoScore]}";
            }

            var leadPlayer = PlayerOneScore > PlayerTwoScore ? "Player 1" : "Player 2";

            if (ScoreDifference == 1)
            {
                return $"Advantage {leadPlayer}";
            }

            return $"{leadPlayer} Wins";
        }
    }
}
