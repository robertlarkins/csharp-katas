using FluentAssertions.Execution;
using Larkins.CSharpKatas.ResultType;

namespace Larkins.CSharpKatas.Tests.Unit.ResultType;

public class UnitResultBehaviours
{
    [Fact]
    public void Success_result_conveys_its_success()
    {
        var sut = UnitResult.Success<string>();

        using (new AssertionScope())
        {
            sut.IsSuccess.Should().BeTrue();
            sut.IsFailure.Should().BeFalse();
        }
    }

    [Fact]
    public void Success_result_does_not_contain_error()
    {
        var sut = UnitResult.Success<string>();

        sut.Invoking(result => result.Error)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("This is a success result. There is no Error.");
    }

    [Fact]
    public void Failure_result_conveys_its_failure()
    {
        var sut = UnitResult.Failure(string.Empty);

        using (new AssertionScope())
        {
            sut.IsSuccess.Should().BeFalse();
            sut.IsFailure.Should().BeTrue();
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData("Failure")]
    public void Failure_result_contains_error<TError>(TError errorValue)
    {
        var sut = UnitResult.Failure(errorValue);

        sut.Error.Should().Be(errorValue);
    }

    [Fact]
    public void Failure_can_be_implicitly_converted_from_type()
    {
        var error = 100;
        UnitResult<int> sut = error;

        sut.Error.Should().Be(error);
    }
}
