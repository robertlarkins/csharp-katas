namespace Larkins.CSharpKatas.TenPinBowling;

/// <summary>
/// A bowling frame consisting of two rolls.
/// </summary>
public class NonFinalBowlingFrame : IBowlingFrame
{
    private readonly int[] pinsKnockedDownPerRoll = new int[2];

    private int rollsAdded = 0;

    /// <inheritdoc />
    public int TotalPinsKnockedDown => pinsKnockedDownPerRoll.Sum();

    /// <inheritdoc />
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

    /// <inheritdoc />
    public void AddRoll(int pinsKnockedDown)
    {
        if (IsComplete)
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
