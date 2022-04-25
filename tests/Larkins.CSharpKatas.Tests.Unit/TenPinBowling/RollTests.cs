using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.Tests.Unit.TenPinBowling;

public class RollTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void The_number_of_pins_knocked_down_cannot_be_outside_range_of_0_to_ten(int pinsKnockedDown)
    {
        var result = Roll.Create(pinsKnockedDown);

        result.IsFailure.Should().BeTrue();
        result.Error.Should()
            .Be("The number of pins knocked down must be between 0 and 10 (inclusive).");
    }
}
