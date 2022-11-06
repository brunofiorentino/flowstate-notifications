using BenchmarkDotNet.Attributes;

namespace Flowstate.Notifications.Tests.Benchmarks;

[MemoryDiagnoser]
public class NotificationsBenchmarks
{
    [Benchmark]
    public Result<int> Valued_success_Result() =>
        Result<int>.Success(123);

    [Benchmark]
    public Result Valueless_success_Result() =>
        Result.Success();

    [Benchmark]
    public Result Valueless_failure_Result_without_details() =>
        Result.Failure();

    [Benchmark]
    public Result Valueless_failure_Result_with_one_detail() =>
        Result.Failure("a error via value result");

    [Benchmark]
    public Result Valueless_failure_Result_with_ten_details() => Result.Failure
    (
        "failure description 001",
        "failure description 002",
        "failure description 003",
        "failure description 004",
        "failure description 005",
        "failure description 006",
        "failure description 007",
        "failure description 008",
        "failure description 009",
        "failure description 010"
    );
}