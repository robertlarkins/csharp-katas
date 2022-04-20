using System;
using System.Linq;

namespace Larkins.CSharpKatas.TenPinBowling;

/// <summary>
/// A bowling frame consisting of two rolls.
/// </summary>
public class NonFinalBowlingFrame
{
    private readonly int[] pinsKnockedDownPerRoll = new int[2];

    private int rollsAdded = 0;

    /// <summary>
    /// Gets the number of pins knocked down in this frame.
    /// </summary>
    public int TotalPinsKnockedDown => pinsKnockedDownPerRoll.Sum();

    /// <summary>
    /// Gets a value indicating whether this frame is complete.
    /// </summary>
    public bool IsComplete => rollsAdded == 2 || IsStrike;

    /// <summary>
    /// Gets a value indicating whether this is an open frame.
    /// An open frame is one where the player makes neither a spare nor a strike.
    /// </summary>
    public bool IsOpenFrame => TotalPinsKnockedDown < 10;

    /// <summary>
    /// Gets a value indicating whether this frame is a spare.
    /// </summary>
    public bool IsSpare => TotalPinsKnockedDown == 10 && rollsAdded == 2;

    /// <summary>
    /// Gets a value indicating whether this frame is a strike.
    /// </summary>
    public bool IsStrike => pinsKnockedDownPerRoll[0] == 10;

    /// <summary>
    /// Add the number of pins knocked down in the roll to the frame.
    /// </summary>
    /// <param name="pinsKnockedDown">The number of pins knocked down in a roll.</param>
    /// <exception cref="InvalidOperationException">If trying to add another roll and no more can be added.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If the number of pins to knock down is outside the 0 to 10 range.</exception>
    public void AddRoll(int pinsKnockedDown)
    {
        if (IsStrike || rollsAdded == 2)
        {
            throw new InvalidOperationException("No more rolls can be added.");
        }

        if (rollsAdded == 1 && pinsKnockedDownPerRoll[0] + pinsKnockedDown > 10)
        {
            throw new ArgumentOutOfRangeException(
                nameof(pinsKnockedDown), "The second roll cannot make total pins knocked down be greater than ten.");
        }

        if (pinsKnockedDown is < 0 or > 10)
        {
            throw new ArgumentOutOfRangeException(
                nameof(pinsKnockedDown), "The number of pins knocked down must be between 0 and 10 (inclusive).");
        }

        pinsKnockedDownPerRoll[rollsAdded] = pinsKnockedDown;
        rollsAdded++;
    }
}
