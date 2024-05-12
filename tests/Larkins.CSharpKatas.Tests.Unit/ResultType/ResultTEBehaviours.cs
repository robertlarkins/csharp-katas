using FluentAssertions.Execution;
using Larkins.CSharpKatas.ResultType;

namespace Larkins.CSharpKatas.Tests.Unit.ResultType;

public class ResultTEBehaviours
{
    [Fact]
    public void Success_result_conveys_its_success()
    {
        var successValue = 100;
        var sut = Result.Success<int, int>(successValue);

        using (new AssertionScope())
        {
            sut.IsSuccess.Should().BeTrue();
            sut.IsFailure.Should().BeFalse();
        }
    }

    [Fact]
    public void Success_result_does_not_contain_error()
    {
        var successValue = 100;
        var sut = Result.Success<int, int>(successValue);

        sut.Invoking(result => result.Error)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("This is a success result. There is no Error.");
    }

    [Theory]
    [InlineData(100)]
    [InlineData("Success")]
    public void Success_result_contains_value<T>(T successValue)
    {
        var sut = Result.Success<T, int>(successValue);

        sut.Value.Should().Be(successValue);
    }

    [Fact]
    public void Success_result_can_be_implicitly_converted_from_type()
    {
        var successValue = 100;
        Result<int, int> sut = successValue;

        sut.Value.Should().Be(successValue);
    }

    [Fact]
    public void Failure_result_conveys_its_failure()
    {
        var errorValue = -1;
        var sut = Result.Failure<int, int>(errorValue);

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
        var sut = Result.Failure<int, TError>(errorValue);

        sut.Error.Should().Be(errorValue);
    }

    [Fact]
    public void Failure_result_does_not_contain_value()
    {
        var sut = Result.Failure<int, string>(string.Empty);

        sut.Invoking(result => result.Value)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("This is a failure result. There is no Value.");
    }
}
