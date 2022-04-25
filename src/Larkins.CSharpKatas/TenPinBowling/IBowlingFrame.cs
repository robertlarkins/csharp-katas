using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.TenPinBowling;

/// <summary>
/// The general interface for bowling frames.
/// </summary>
public interface IBowlingFrame
{
    /// <summary>
    /// Gets the number of pins knocked down in this frame.
    /// </summary>
    int TotalPinsKnockedDown { get; }

    /// <summary>
    /// Gets a value indicating whether this frame is complete.
    /// </summary>
    bool IsComplete { get; }

    /// <summary>
    /// Add the number of pins knocked down in the roll to the frame.
    /// </summary>
    /// <param name="roll">The number of pins knocked down in a roll.</param>
    /// <exception cref="InvalidOperationException">If trying to add another roll and no more can be added.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If the number of pins to knock down is outside the 0 to 10 range.</exception>
    public void AddRoll(Roll roll);
}
