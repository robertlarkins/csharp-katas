using Larkins.CSharpKatas.ResultType;

namespace Larkins.CSharpKatas.TenPinBowling.ValueObjects;

/// <summary>
/// Represents a roll where 0 to 10 pins have been knocked down.
/// </summary>
public record Roll
{
    private Roll(int pinsKnockedDown)
    {
        PinsKnockedDown = pinsKnockedDown;
    }

    /// <summary>
    /// Gets the number of pins knocked down in this roll.
    /// </summary>
    public int PinsKnockedDown { get; }

    /// <summary>
    /// Creates an instance of a ten pin bowling roll.
    /// </summary>
    /// <param name="pinsKnockedDown">The number of pins knocked down in this roll.</param>
    /// <returns>Result of creating this roll.</returns>
    public static Result<Roll> Create(int pinsKnockedDown)
    {
        if (pinsKnockedDown is < 0 or > 10)
        {
            return Result.Failure<Roll>(
                "The number of pins knocked down must be between 0 and 10 (inclusive).");
        }

        return new Roll(pinsKnockedDown);
    }
}
