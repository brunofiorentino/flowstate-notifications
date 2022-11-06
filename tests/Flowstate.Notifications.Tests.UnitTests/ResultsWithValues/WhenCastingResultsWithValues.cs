namespace Flowstate.Notifications.Tests.UnitTests.ResultsWithValues;

public class WhenCastingResultsWithValues
{
    private readonly int? _someValue = 123;

    [Fact]
    public void SuccessResult_implicit_boolean_cast_produces_expected_value()
    {
        var result = Result<int?>.Success(_someValue);
        Assert.True(result);
    }

    [Fact]
    public void FailureResult_implicit_boolean_cast_produces_expected_value()
    {
        var result = Result<int?>.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureResult_with_value_cast_produces_expected_value()
    {
        var originalResult = Result<int?>.Failure("err1", "err2");
        var newResult = originalResult.AsValuedFailure<long?>();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.FailureDetails.Count, newResult.FailureDetails.Count);

        Assert.All(
            originalResult.FailureDetails.Zip(newResult.FailureDetails),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void FailureResult_without_value_cast_produces_expected_value()
    {
        var originalResult = Result<int?>.Failure("err1", "err2");
        var newResult = originalResult.AsValuelessFailure();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.FailureDetails.Count, newResult.FailureDetails.Count);

        Assert.All(
            originalResult.FailureDetails.Zip(newResult.FailureDetails),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void Trying_to_force_FailureResult_cast_on_SuccessResult_throws()
    {
        var originalResult = Result<int?>.Success(123);
        var exception = Assert.Throws<Exception>(() => originalResult.AsValuedFailure<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult, exception.ToString());
    }
}