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
    public Result Valueless_failure_Result_with_0001_details() =>
        Result.Failure("failure description 0");

    [Benchmark]
    public Result Valueless_failure_Result_with_0002_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1"
    );

    [Benchmark]
    public Result Valueless_failure_Result_with_0004_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1",
        "failure description 2",
        "failure description 3"
    );

    [Benchmark]
    public Result Valueless_failure_Result_with_0008_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1",
        "failure description 2",
        "failure description 3",
        "failure description 4",
        "failure description 5",
        "failure description 6",
        "failure description 7"
    );

    [Benchmark]
    public Result Valueless_failure_Result_with_0016_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1",
        "failure description 2",
        "failure description 3",
        "failure description 4",
        "failure description 5",
        "failure description 6",
        "failure description 7",
        "failure description 8",
        "failure description 9",
        "failure description 10",
        "failure description 11",
        "failure description 12",
        "failure description 13",
        "failure description 14",
        "failure description 15"
    );

    [Benchmark]
    public Result Valueless_failure_Result_with_0032_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1",
        "failure description 2",
        "failure description 3",
        "failure description 4",
        "failure description 5",
        "failure description 6",
        "failure description 7",
        "failure description 8",
        "failure description 9",
        "failure description 10",
        "failure description 11",
        "failure description 12",
        "failure description 13",
        "failure description 14",
        "failure description 15",
        "failure description 16",
        "failure description 17",
        "failure description 18",
        "failure description 19",
        "failure description 20",
        "failure description 21",
        "failure description 22",
        "failure description 23",
        "failure description 24",
        "failure description 25",
        "failure description 26",
        "failure description 27",
        "failure description 28",
        "failure description 29",
        "failure description 30",
        "failure description 31"
    );

    [Benchmark]
    public Result Valueless_failure_Result_with_0064_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1",
        "failure description 2",
        "failure description 3",
        "failure description 4",
        "failure description 5",
        "failure description 6",
        "failure description 7",
        "failure description 8",
        "failure description 9",
        "failure description 10",
        "failure description 11",
        "failure description 12",
        "failure description 13",
        "failure description 14",
        "failure description 15",
        "failure description 16",
        "failure description 17",
        "failure description 18",
        "failure description 19",
        "failure description 20",
        "failure description 21",
        "failure description 22",
        "failure description 23",
        "failure description 24",
        "failure description 25",
        "failure description 26",
        "failure description 27",
        "failure description 28",
        "failure description 29",
        "failure description 30",
        "failure description 31",
        "failure description 32",
        "failure description 33",
        "failure description 34",
        "failure description 35",
        "failure description 36",
        "failure description 37",
        "failure description 38",
        "failure description 39",
        "failure description 40",
        "failure description 41",
        "failure description 42",
        "failure description 43",
        "failure description 44",
        "failure description 45",
        "failure description 46",
        "failure description 47",
        "failure description 48",
        "failure description 49",
        "failure description 50",
        "failure description 51",
        "failure description 52",
        "failure description 53",
        "failure description 54",
        "failure description 55",
        "failure description 56",
        "failure description 57",
        "failure description 58",
        "failure description 59",
        "failure description 60",
        "failure description 61",
        "failure description 62",
        "failure description 63"
    );



    [Benchmark]
    public Result Valueless_failure_Result_with_0128_details() => Result.Failure
    (
        "failure description 0",
        "failure description 1",
        "failure description 2",
        "failure description 3",
        "failure description 4",
        "failure description 5",
        "failure description 6",
        "failure description 7",
        "failure description 8",
        "failure description 9",
        "failure description 10",
        "failure description 11",
        "failure description 12",
        "failure description 13",
        "failure description 14",
        "failure description 15",
        "failure description 16",
        "failure description 17",
        "failure description 18",
        "failure description 19",
        "failure description 20",
        "failure description 21",
        "failure description 22",
        "failure description 23",
        "failure description 24",
        "failure description 25",
        "failure description 26",
        "failure description 27",
        "failure description 28",
        "failure description 29",
        "failure description 30",
        "failure description 31",
        "failure description 32",
        "failure description 33",
        "failure description 34",
        "failure description 35",
        "failure description 36",
        "failure description 37",
        "failure description 38",
        "failure description 39",
        "failure description 40",
        "failure description 41",
        "failure description 42",
        "failure description 43",
        "failure description 44",
        "failure description 45",
        "failure description 46",
        "failure description 47",
        "failure description 48",
        "failure description 49",
        "failure description 50",
        "failure description 51",
        "failure description 52",
        "failure description 53",
        "failure description 54",
        "failure description 55",
        "failure description 56",
        "failure description 57",
        "failure description 58",
        "failure description 59",
        "failure description 60",
        "failure description 61",
        "failure description 62",
        "failure description 63",
        "failure description 64",
        "failure description 65",
        "failure description 66",
        "failure description 67",
        "failure description 68",
        "failure description 69",
        "failure description 70",
        "failure description 71",
        "failure description 72",
        "failure description 73",
        "failure description 74",
        "failure description 75",
        "failure description 76",
        "failure description 77",
        "failure description 78",
        "failure description 79",
        "failure description 80",
        "failure description 81",
        "failure description 82",
        "failure description 83",
        "failure description 84",
        "failure description 85",
        "failure description 86",
        "failure description 87",
        "failure description 88",
        "failure description 89",
        "failure description 90",
        "failure description 91",
        "failure description 92",
        "failure description 93",
        "failure description 94",
        "failure description 95",
        "failure description 96",
        "failure description 97",
        "failure description 98",
        "failure description 99",
        "failure description 100",
        "failure description 11",
        "failure description 12",
        "failure description 13",
        "failure description 14",
        "failure description 15",
        "failure description 16",
        "failure description 17",
        "failure description 18",
        "failure description 19",
        "failure description 110",
        "failure description 111",
        "failure description 112",
        "failure description 113",
        "failure description 114",
        "failure description 115",
        "failure description 116",
        "failure description 117",
        "failure description 118",
        "failure description 119",
        "failure description 120",
        "failure description 121",
        "failure description 122",
        "failure description 123",
        "failure description 124",
        "failure description 125",
        "failure description 126",
        "failure description 127"
    );
}