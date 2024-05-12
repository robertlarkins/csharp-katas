namespace Larkins.CSharpKatas.ResultType;

#pragma warning disable SA1402 // FileMayOnlyContainASingleType
public record UnitResult<TError>
{
    private readonly TError? error;

    internal UnitResult(bool isSuccess, TError? error = default)
    {
        IsSuccess = isSuccess;
        this.error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public TError Error => IsFailure
        ? error!
        : throw new InvalidOperationException("This is a success result. There is no Error.");

    public static implicit operator UnitResult<TError>(TError error) => UnitResult.Failure(error);
}

/// <summary>
/// This UnitResult class is only here so that a UnitResult&lt;TError&gt; can be created by going
/// UnitResult.Failure(MyError) instead of having to go UnitResult&lt;MyError&gt;.Failure(MyError).
/// </summary>
public static class UnitResult
{
    public static UnitResult<TError> Success<TError>() => new(true);

    public static UnitResult<TError> Failure<TError>(TError error)
        => new(false, error);
}
#pragma warning restore SA1402 // FileMayOnlyContainASingleType
