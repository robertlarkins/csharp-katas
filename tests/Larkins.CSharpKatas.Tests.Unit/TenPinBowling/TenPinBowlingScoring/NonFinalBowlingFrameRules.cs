using Larkins.CSharpKatas.TenPinBowling;
using Larkins.CSharpKatas.TenPinBowling.ValueObjects;

namespace Larkins.CSharpKatas.Tests.Unit.TenPinBowling.TenPinBowlingScoring;

public class NonFinalBowlingFrameRules
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 10, 10)]
    [InlineData(1, 1, 2)]
    public void Total_number_of_pins_knocked_down_is_the_sum_of_the_two_rolls(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown,
        int expected)
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(rollOnePinsKnockedDown).Value);
        sut.AddRoll(Roll.Create(rollTwoPinsKnockedDown).Value);

        sut.TotalPinsKnockedDown.Should().Be(expected);
    }

    [Fact]
    public void Total_number_of_pins_knocked_down_is_ten_if_the_frame_is_a_strike()
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(10).Value);

        sut.TotalPinsKnockedDown.Should().Be(10);
    }

    [Fact]
    public void Frame_is_complete_if_the_first_roll_is_a_strike()
    {
        var sut = new NonFinalBowlingFrame();
        sut.AddRoll(Roll.Create(10).Value);

        sut.IsComplete.Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 10)]
    [InlineData(1, 1)]
    public void Frame_is_complete_after_two_rolls(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown)
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(rollOnePinsKnockedDown).Value);
        sut.AddRoll(Roll.Create(rollTwoPinsKnockedDown).Value);

        sut.IsComplete.Should().BeTrue();
    }

    [Fact]
    public void Strike_occurs_if_first_roll_knocks_over_ten_pins()
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(10).Value);

        sut.IsStrike.Should().BeTrue();
        sut.IsSpare.Should().BeFalse();
        sut.IsOpenFrame.Should().BeFalse();
    }

    [Theory]
    [InlineData(5, 5)]
    [InlineData(0, 10)]
    [InlineData(9, 1)]
    [InlineData(1, 9)]
    public void Spare_occurs_if_two_rolls_knocks_over_ten_pins(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown)
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(rollOnePinsKnockedDown).Value);
        sut.AddRoll(Roll.Create(rollTwoPinsKnockedDown).Value);

        sut.IsSpare.Should().BeTrue();
        sut.IsStrike.Should().BeFalse();
        sut.IsOpenFrame.Should().BeFalse();
    }

    [Theory]
    [InlineData(5, 4)]
    [InlineData(0, 9)]
    [InlineData(9, 0)]
    [InlineData(1, 8)]
    public void Frame_is_open_if_less_than_ten_pins_are_knocked_down_in_two_rolls(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown)
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(rollOnePinsKnockedDown).Value);
        sut.AddRoll(Roll.Create(rollTwoPinsKnockedDown).Value);

        sut.IsOpenFrame.Should().BeTrue();
        sut.IsSpare.Should().BeFalse();
        sut.IsStrike.Should().BeFalse();
    }

    [Theory]
    [InlineData(5, 7)]
    [InlineData(9, 2)]
    public void Second_roll_cannot_make_total_pins_knocked_down_be_greater_than_ten(
        int rollOnePinsKnockedDown,
        int rollTwoPinsKnockedDown)
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(rollOnePinsKnockedDown).Value);

        var action = () => sut.AddRoll(Roll.Create(rollTwoPinsKnockedDown).Value);

        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("roll")
            .WithMessage("The second roll cannot make total pins knocked down be greater than ten. (Parameter 'roll')");
    }

    [Fact]
    public void Subsequent_rolls_cannot_be_added_if_the_first_was_a_strike()
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(10).Value);

        var action = () => sut.AddRoll(Roll.Create(10).Value);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("No more rolls can be added.");
    }

    [Fact]
    public void Frame_can_only_have_two_rolls_added()
    {
        var sut = new NonFinalBowlingFrame();

        sut.AddRoll(Roll.Create(4).Value);
        sut.AddRoll(Roll.Create(2).Value);

        var action = () => sut.AddRoll(Roll.Create(2).Value);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("No more rolls can be added.");
    }
}
