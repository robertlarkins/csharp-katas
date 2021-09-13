using System;
using System.Collections.Generic;

namespace Larkins.CSharpKatas
{
    /// <summary>
    /// Wraps a Tennis Game.
    /// It is based on this Kata: https://josepaumard.github.io/katas/intermediate/tennis-kata.html.
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
        /// Gets or sets player one's score.
        /// </summary>
        public int PlayerOneScore { get; set; }

        /// <summary>
        /// Gets or sets the player two's score.
        /// </summary>
        public int PlayerTwoScore { get; set; }

        /// <summary>
        /// Gets the current score of the Tennis game.
        /// </summary>
        /// <returns>The score.</returns>
        public string GetScore()
        {
            var isEqualScores = PlayerOneScore == PlayerTwoScore;

            if (PlayerOneScore <= 3 && PlayerTwoScore <= 3 && !isEqualScores)
            {
                return $"{scoreLookup[PlayerOneScore]}-{scoreLookup[PlayerTwoScore]}";
            }

            if (isEqualScores)
            {
                if (PlayerOneScore < 3)
                {
                    return $"{scoreLookup[PlayerOneScore]}-All";
                }

                return "Deuce";
            }

            var playerInTheLead = WhichPlayerIsInTheLead();

            if (Math.Abs(PlayerOneScore - PlayerTwoScore) == 1)
            {
                return $"Advantage {playerInTheLead}";
            }

            return $"{playerInTheLead} Wins";
        }

        private string WhichPlayerIsInTheLead() => PlayerOneScore > PlayerTwoScore ? "Player 1" : "Player 2";
    }
}
