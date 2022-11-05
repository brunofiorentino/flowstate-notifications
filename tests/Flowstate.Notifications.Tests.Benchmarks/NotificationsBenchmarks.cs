using BenchmarkDotNet.Attributes;

namespace Flowstate.Notifications.Tests.Benchmarks;

[MemoryDiagnoser]
public class NotificationsBenchmarks
{
    [Benchmark]
    public Result ValueNotificationOfSuccess() =>
        Result.Success();

    [Benchmark]
    public Result ValueNotificationOfFailureWithoutDetails() =>
        Result.Failure();

    [Benchmark]
    public Result ValueNotificationOfFailureWithOneDetail() =>
        Result.Failure("a error via value result");

    [Benchmark]
    public Result ValueNotificationOfFailureWithManyDetails() => Result.Failure
    (
        "a error via value result 001",
        "a error via value result 002",
        "a error via value result 003",
        "a error via value result 004",
        "a error via value result 005",
        "a error via value result 006",
        "a error via value result 007",
        "a error via value result 008",
        "a error via value result 009",
        "a error via value result 010"
    );

    [Benchmark]
    public References.Result ReferenceNotificationOfSuccessWithoutDetails() =>
        References.Result.Success();

    [Benchmark]
    public References.Result ReferenceNotificationOfFailureWithoutDetails() =>
    References.Result.Failure();

    [Benchmark]
    public References.Result ReferenceNotificationOfFailureWithOneDetail() =>
        References.Result.Failure("a error via ref result");

    [Benchmark]
    public References.Result ReferenceNotificationOfFailureWithManyDetails() => References.Result.Failure
    (
        "a error via reference result 001",
        "a error via reference result 002",
        "a error via reference result 003",
        "a error via reference result 004",
        "a error via reference result 005",
        "a error via reference result 006",
        "a error via reference result 007",
        "a error via reference result 008",
        "a error via reference result 009",
        "a error via reference result 010"
    );
}
