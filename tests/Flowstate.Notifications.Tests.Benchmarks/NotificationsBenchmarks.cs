using BenchmarkDotNet.Attributes;

namespace Flowstate.Notifications.Tests.Benchmarks;

[MemoryDiagnoser]
public class NotificationsBenchmarks
{
    [Benchmark]
    public Result<int> Valued_SuccessResult() =>
        Result<int>.Success(123);

    [Benchmark]
    public Result Valueless_SuccessResult() =>
        Result.Success();

    [Benchmark]
    public Result Valueless_FailureResult_without_details() =>
        Result.Failure();

    [Benchmark]
    public Result Valueless_FailureResult_with_one_detail() =>
        Result.Failure("a error via value result");

    [Benchmark]
    public Result Valueless_FailureResult_with_many_details() => Result.Failure
    (
        "a failure description 001",
        "a failure description 002",
        "a failure description 003",
        "a failure description 004",
        "a failure description 005",
        "a failure description 006",
        "a failure description 007",
        "a failure description 008",
        "a failure description 009",
        "a failure description 010"
    );
}
