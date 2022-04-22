using Larkins.CSharpKatas.TenPinBowling;

namespace Larkins.CSharpKatas.Tests.Unit.TenPinBowlingScoring;

public class FinalBowlingFrameRules
{
    [Theory]
    [InlineData(0, 0, 2)]
    [InlineData(4, 5, 0)]
    public void Third_roll_cannot_be_added_if_total_of_first_two_rolls_is_less_than_ten(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown,
        int rollThreePinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);

        var action = () => sut.AddRoll(rollThreePinsKnockedDown);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("No more rolls can be added.");
    }

    [Fact]
    public void Frame_can_only_have_three_rolls_added()
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(10);
        sut.AddRoll(1);
        sut.AddRoll(2);

        var action = () => sut.AddRoll(3);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("No more rolls can be added.");
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(0, 0)]
    public void Frame_is_complete_if_three_rolls_are_added_and_the_first_is_a_strike(
        int rollTwoPinsKnockedDown,
        int rollThreePinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(10);
        sut.AddRoll(rollTwoPinsKnockedDown);
        sut.AddRoll(rollThreePinsKnockedDown);

        sut.IsComplete.Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(1, 9, 1)]
    [InlineData(9, 1, 6)]
    [InlineData(4, 6, 10)]
    public void Frame_is_complete_if_three_rolls_are_added_and_the_first_two_are_a_spare(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown,
        int rollThreePinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);
        sut.AddRoll(rollThreePinsKnockedDown);

        sut.IsComplete.Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 9)]
    [InlineData(9, 0)]
    [InlineData(4, 1)]
    public void Frame_is_complete_if_first_two_rolls_are_added_and_their_total_is_less_than_ten(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);

        sut.IsComplete.Should().BeTrue();
    }

    [Theory]
    [InlineData(10, 0, 0, 10)]
    [InlineData(10, 10, 1, 21)]
    [InlineData(10, 10, 10, 30)]
    [InlineData(10, 1, 1, 12)]
    public void Total_number_of_pins_knocked_down_is_the_sum_of_three_rolls_if_the_first_roll_is_a_strike(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown,
        int rollThreePinsKnockedDown,
        int expected)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);
        sut.AddRoll(rollThreePinsKnockedDown);

        sut.TotalPinsKnockedDown.Should().Be(expected);
    }

    [Theory]
    [InlineData(0, 10, 0, 10)]
    [InlineData(9, 1, 1, 11)]
    [InlineData(5, 5, 10, 20)]
    public void Total_number_of_pins_knocked_down_is_the_sum_of_three_rolls_if_the_first_two_rolls_are_a_spare(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown,
        int rollThreePinsKnockedDown,
        int expected)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);
        sut.AddRoll(rollThreePinsKnockedDown);

        sut.TotalPinsKnockedDown.Should().Be(expected);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 1, 2)]
    [InlineData(5, 4, 9)]
    public void Total_number_of_pins_knocked_down_is_the_sum_of_the_two_rolls_if_their_total_is_less_than_ten(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown,
        int expected)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);

        sut.TotalPinsKnockedDown.Should().Be(expected);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    public void Frame_is_incomplete_if_only_one_roll_added(int rollOnePinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);

        sut.IsComplete.Should().BeFalse();
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(10, 0)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    public void Frame_is_incomplete_if_total_of_first_two_rolls_are_ten_or_more(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        sut.AddRoll(rollOnePinsKnockedDown);
        sut.AddRoll(rollTwoPinsKnockedDown);

        sut.IsComplete.Should().BeFalse();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void An_added_roll_cannot_be_outside_range_of_0_to_ten_pins_knocked_down(int pinsKnockedDown)
    {
        var sut = new FinalBowlingFrame();

        var action = () => sut.AddRoll(pinsKnockedDown);

        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("pinsKnockedDown")
            .WithMessage("The number of pins knocked down must be between 0 and 10 (inclusive). (Parameter 'pinsKnockedDown')");
    }
}
