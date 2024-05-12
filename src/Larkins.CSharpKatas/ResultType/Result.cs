namespace Larkins.CSharpKatas.ResultType;

public partial record Result
{
    private readonly string? error;

    private Result(bool isSuccess, string? error = default)
    {
        IsSuccess = isSuccess;
        this.error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string Error => IsFailure
        ? error!
        : throw new InvalidOperationException("This is a success result. There is no Error.");

    public static Result Success() => new(true);

    public static Result Failure(string errorMessage) => new(false, errorMessage);
}
