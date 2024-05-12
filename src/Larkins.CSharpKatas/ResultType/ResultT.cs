namespace Larkins.CSharpKatas.ResultType;

#pragma warning disable SA1402 // FileMayOnlyContainASingleType
#pragma warning disable SA1649 // FileNameMustMatchTypeName
public record Result<T>
#pragma warning restore SA1649 // FileNameMustMatchTypeName
{
    private readonly T? value;
    private readonly string? error;

    // The reason that `default` doesn't just provide null is because the generic type T
    // could be either a class or a struct. There is ongoing discussion around this:
    // https://github.com/dotnet/csharplang/discussions/2194
    // The work-around used here is to ignore the provided value and set it to null if
    // isSuccess is false.
    internal Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        this.value = value;
        this.error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("This is a failure result. There is no Value.");

    public string Error => IsFailure
        ? error!
        : throw new InvalidOperationException("This is a success result. There is no Error.");

    public static implicit operator Result<T>(T value) => Result.Success(value);
}

public partial record Result
{
    public static Result<T> Success<T>(T value)
        => new(
        isSuccess: true,
        value: value,
        error: default);

    public static Result<T> Failure<T>(string errorMessage)
        => new(
        isSuccess: false,
        value: default,
        error: errorMessage);
}
#pragma warning restore SA1402 // FileMayOnlyContainASingleType
