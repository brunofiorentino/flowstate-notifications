namespace Flowstate.Notifications.Tests.UnitTests.ValuedResults;

public class WhenCastingValuedResults
{
    private readonly int? _someValue = 123;

    [Fact]
    public void SuccessResultToBooleanImplicitCastProducesExpectedValue()
    {
        var result = Result<int?>.Success(_someValue);
        Assert.True(result);
    }

    [Fact]
    public void FailureResultToBooleanImplicitCastProducesExpectedValue()
    {
        var result = Result<int?>.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureResultToOtherValuedResultCastProducesExpectedValue()
    {
        var originalResult = Result<int?>.Failure("err1", "err2");
        var newResult = originalResult.Cast<long?>();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.FailureDetails.Count, newResult.FailureDetails.Count);

        Assert.All(
            originalResult.FailureDetails.Zip(newResult.FailureDetails),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void SuccessResultToValuelessResultCastProducesExpectedValue()
    {
        var originalResult = Result<int?>.Success(_someValue);
        var newResult = originalResult.Cast();

        Assert.True(newResult.Succeeded);
        Assert.Empty(newResult.FailureDetails);
    }

    [Fact]
    public void FailureResultToValuelessResultCastProducesExpectedValue()
    {
        var originalResult = Result<int?>.Failure("err1", "err2");
        var newResult = originalResult.Cast();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.FailureDetails.Count, newResult.FailureDetails.Count);

        Assert.All(
            originalResult.FailureDetails.Zip(newResult.FailureDetails),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void TryingToForceSuccessResultToFailureResultCastThrows()
    {
        var originalResult = Result<int?>.Success(123);
        var exception = Assert.Throws<Exception>(() => originalResult.Cast<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult, exception.ToString());
    }
}