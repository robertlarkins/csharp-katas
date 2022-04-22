namespace Larkins.CSharpKatas.TenPinBowling;

/// <summary>
/// The final bowling frame consisting of up to three rolls.
/// </summary>
public class FinalBowlingFrame : IBowlingFrame
{
    private readonly int[] pinsKnockedDownPerRoll = new int[3];

    private int rollsAdded = 0;

    /// <inheritdoc />
    public int TotalPinsKnockedDown => pinsKnockedDownPerRoll.Sum();

    /// <inheritdoc />
    public bool IsComplete => rollsAdded == 3 || rollsAdded == 2 && TotalPinsKnockedDown < 10;

    /// <inheritdoc />
    public void AddRoll(int pinsKnockedDown)
    {
        if (IsComplete)
        {
            throw new InvalidOperationException("No more rolls can be added.");
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
