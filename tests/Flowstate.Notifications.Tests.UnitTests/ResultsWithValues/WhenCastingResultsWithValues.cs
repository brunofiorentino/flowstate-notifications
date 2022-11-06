namespace Flowstate.Notifications.Tests.UnitTests.ResultsWithValues;

public class WhenCastingResultsWithValues
{
    private readonly int? _someValue = 123;

    [Fact]
    public void Success_implicit_boolean_cast_produces_expected_value()
    {
        var result = Result<int?>.Success(_someValue);
        Assert.True(result);
    }

    [Fact]
    public void Failure_implicit_boolean_cast_produces_expected_value()
    {
        var result = Result<int?>.Failure();
        Assert.False(result);
    }

    [Fact]
    public void Failure_with_value_cast_produces_expected_value()
    {
        var originalResult = Result<int?>.Failure("err1", "err2");
        var newResult = originalResult.CastFailure<long?>();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.Details.Count, newResult.Details.Count);

        Assert.All(
            originalResult.Details.Zip(newResult.Details),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void Failure_without_value_cast_produces_expected_value()
    {
        var originalResult = Result<int?>.Failure("err1", "err2");
        var newResult = originalResult.CastFailure();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.Details.Count, newResult.Details.Count);

        Assert.All(
            originalResult.Details.Zip(newResult.Details),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void Trying_to_force_failure_cast_on_success_result_throws()
    {
        var originalResult = Result<int?>.Success(123);
        var exception = Assert.Throws<Exception>(() => originalResult.CastFailure<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSucceededResultAsFailure, exception.ToString());
    }
}