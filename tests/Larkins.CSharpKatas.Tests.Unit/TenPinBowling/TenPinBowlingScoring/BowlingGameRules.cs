using Larkins.CSharpKatas.TenPinBowling;
using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.Tests.Unit.TenPinBowling.TenPinBowlingScoring;

public class BowlingGameRules
{
    [Fact]
    public void Bowling_game_final_score_is_correctly_calculated()
    {
        // Arrange
        var frames = new List<NonFinalBowlingFrame>
        {
        };

        // var sut = new BowlingGame(frames);

        // Act

        // Assert
    }

    // Want to get the score at each frame

    // The current frame value increases when a frame is completed

    // The game is complete when the tenth frame is complete

    // When a roll is added to the bowling game

    // The total game score is correct for the added rolls
    [Theory]
    [MemberData(nameof(CurrentFrameFromRollsScenarios))]
    public void Current_frame_is_one_more_than_the_number_of_frames_completed_from_adding_rolls_up_to_frame_nine(
        int[] pinsKnockedDownInEachRoll,
        int expectedCurrentFrame)
    {
        var sut = new BowlingGame();

        foreach (var pinsKnockedDown in pinsKnockedDownInEachRoll)
        {
            sut.AddRoll(Roll.Create(pinsKnockedDown).Value);
        }

        sut.CurrentFrameNumber.Should().Be(expectedCurrentFrame);
    }

    [Fact]
    public void Current_frame_stays_at_ten_when_game_is_complete()
    {
        var pinsKnockedDownInEachRoll = new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        var sut = CreateBowlingGameFromPinsKnockedDown(pinsKnockedDownInEachRoll);

        sut.IsGameFinished.Should().BeTrue();
        sut.CurrentFrameNumber.Should().Be(10);
    }

    private static TheoryData<int[], int> CurrentFrameFromRollsScenarios()
    {
        return new TheoryData<int[], int>
        {
            { Array.Empty<int>(), 1 },
            { new[] { 9 }, 1 },
            { new[] { 10 }, 2 },
            { new[] { 4, 3, 10, 7, 3, 5, 0 }, 5 },
            { new[] { 4, 3, 10, 7, 3, 5, 0, 0 }, 5 },
            { new[] { 10, 10, 10, 10, 10, 10, 10, 10 }, 9 },
            { new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 10 },
            { new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 10 },
            { new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 10 }
        };
    }

    private BowlingGame CreateBowlingGameFromPinsKnockedDown(int[] pinsKnockedDownInEachRoll)
    {
        var bowlingGame = new BowlingGame();

        foreach (var pinsKnockedDown in pinsKnockedDownInEachRoll)
        {
            bowlingGame.AddRoll(Roll.Create(pinsKnockedDown).Value);
        }

        return bowlingGame;
    }
}
