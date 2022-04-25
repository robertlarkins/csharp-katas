using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.TenPinBowling;

/// <summary>
/// The final bowling frame consisting of up to three rolls.
/// </summary>
public class FinalBowlingFrame : IBowlingFrame
{
    private readonly List<Roll> rolls = new();

    /// <inheritdoc />
    public int TotalPinsKnockedDown => rolls.Sum(roll => roll.PinsKnockedDown);

    /// <inheritdoc />
    public bool IsComplete => rolls.Count == 3 || rolls.Count == 2 && TotalPinsKnockedDown < 10;

    /// <inheritdoc />
    public void AddRoll(Roll roll)
    {
        if (IsComplete)
        {
            throw new InvalidOperationException("No more rolls can be added.");
        }

        rolls.Add(roll);
    }
}
