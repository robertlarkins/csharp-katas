using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.TenPinBowling;

/// <summary>
/// A bowling frame consisting of two rolls.
/// </summary>
public class NonFinalBowlingFrame : IBowlingFrame
{
    private readonly List<Roll> rolls = new();

    /// <inheritdoc />
    public int TotalPinsKnockedDown => rolls.Sum(roll => roll.PinsKnockedDown);

    /// <inheritdoc />
    public bool IsComplete => rolls.Count == 2 || IsStrike;

    /// <summary>
    /// Gets a value indicating whether this is an open frame.
    /// An open frame is one where the player makes neither a spare nor a strike.
    /// </summary>
    public bool IsOpenFrame => TotalPinsKnockedDown < 10;

    /// <summary>
    /// Gets a value indicating whether this frame is a spare.
    /// </summary>
    public bool IsSpare => TotalPinsKnockedDown == 10 && rolls.Count == 2;

    /// <summary>
    /// Gets a value indicating whether this frame is a strike.
    /// </summary>
    public bool IsStrike => rolls.Count == 1 && rolls[0].PinsKnockedDown == 10;

    /// <inheritdoc />
    public void AddRoll(Roll roll)
    {
        if (IsComplete)
        {
            throw new InvalidOperationException("No more rolls can be added.");
        }

        if (rolls.Count == 1 && rolls[0].PinsKnockedDown + roll.PinsKnockedDown > 10)
        {
            throw new ArgumentOutOfRangeException(
                nameof(roll), "The second roll cannot make total pins knocked down be greater than ten.");
        }

        rolls.Add(roll);
    }
}
