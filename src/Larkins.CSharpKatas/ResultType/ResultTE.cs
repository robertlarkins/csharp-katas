namespace Larkins.CSharpKatas.ResultType;

#pragma warning disable SA1402 // FileMayOnlyContainASingleType
#pragma warning disable SA1649 // FileNameMustMatchTypeName
public record Result<T, TError>
#pragma warning restore SA1649 // FileNameMustMatchTypeName
{
    private readonly T? value;
    private readonly TError? error;

    internal Result(bool isSuccess, T? value, TError? error)
    {
        IsSuccess = isSuccess;
        this.error = error;
        this.value = value;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("This is a failure result. There is no Value.");

    public TError Error => IsFailure
        ? error!
        : throw new InvalidOperationException("This is a success result. There is no Error.");

    public static implicit operator Result<T, TError>(T value) => Result.Success<T, TError>(value);
}

public partial record Result
{
    public static Result<T, TError> Success<T, TError>(T value) => new(
        isSuccess: true,
        value: value,
        error: default);

    public static Result<T, TError> Failure<T, TError>(TError error) => new(
        isSuccess: false,
        value: default,
        error: error);
}
#pragma warning restore SA1402 // FileMayOnlyContainASingleType
