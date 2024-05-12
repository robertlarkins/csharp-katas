using FluentAssertions.Execution;
using Larkins.CSharpKatas.ResultType;

namespace Larkins.CSharpKatas.Tests.Unit.ResultType;

public class ResultTBehaviours
{
    [Fact]
    public void Success_result_conveys_its_success()
    {
        var successValue = 100;
        var sut = Result.Success(successValue);

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
        var sut = Result.Success(successValue);

        sut.Invoking(result => result.Error)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("This is a success result. There is no Error.");
    }

    [Theory]
    [InlineData(100)]
    [InlineData("Success")]
    public void Success_result_contains_value<T>(T successValue)
    {
        var sut = Result.Success(successValue);

        sut.Value.Should().Be(successValue);
    }

    [Fact]
    public void Success_result_can_be_implicitly_converted_from_type()
    {
        var successValue = 100;
        Result<int> sut = successValue;

        sut.Value.Should().Be(successValue);
    }

    [Fact]
    public void Failure_result_conveys_its_failure()
    {
        var sut = Result.Failure<int>(string.Empty);

        using (new AssertionScope())
        {
            sut.IsSuccess.Should().BeFalse();
            sut.IsFailure.Should().BeTrue();
        }
    }

    [Fact]
    public void Failure_result_contains_error_message()
    {
        var errorMessage = "Failed result.";
        var sut = Result.Failure<int>(errorMessage);

        sut.Error.Should().Be(errorMessage);
    }

    [Fact]
    public void Failure_result_does_not_contain_value()
    {
        var sut = Result.Failure<int>(string.Empty);

        sut.Invoking(result => result.Value)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("This is a failure result. There is no Value.");
    }
}
