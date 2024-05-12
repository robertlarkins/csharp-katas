using FluentAssertions.Execution;
using Larkins.CSharpKatas.ResultType;

namespace Larkins.CSharpKatas.Tests.Unit.ResultType;

public class ResultBehaviours
{
    [Fact]
    public void Success_result_conveys_its_success()
    {
        var sut = Result.Success();

        using (new AssertionScope())
        {
            sut.IsSuccess.Should().BeTrue();
            sut.IsFailure.Should().BeFalse();
        }
    }

    [Fact]
    public void Success_result_does_not_contain_error_message()
    {
        var sut = Result.Success();

        sut.Invoking(result => result.Error)
            .Should().Throw<InvalidOperationException>()
            .WithMessage("This is a success result. There is no Error.");
    }

    [Fact]
    public void Failure_result_conveys_its_failure()
    {
        var sut = Result.Failure(string.Empty);

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
        var sut = Result.Failure(errorMessage);

        sut.Error.Should().Be(errorMessage);
    }
}
