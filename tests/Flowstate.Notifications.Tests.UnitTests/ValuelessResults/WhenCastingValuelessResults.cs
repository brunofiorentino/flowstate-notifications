namespace Flowstate.Notifications.Tests.UnitTests.ValuelessResults;

public class WhenCastingValuelessResults
{
    [Fact]
    public void SuccessResultToBooleanImplicitCastProducesExpectedValue()
    {
        var result = Result.Success();
        Assert.True(result);
    }

    [Fact]
    public void FailureResultToBooleanImplicitCastProducesExpectedValue()
    {
        var result = Result.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureResultToValuedCastProducesExpectedValue()
    {
        var originalResult = Result.Failure("err1", "err2");
        var newResult = originalResult.Cast<long?>();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.FailureDetails.Count, newResult.FailureDetails.Count);

        Assert.All(
            originalResult.FailureDetails.Zip(newResult.FailureDetails),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void TryingToForceSuccessResultToFailureResultCastThrows()
    {
        var originalResult = Result.Success();
        var exception = Assert.Throws<Exception>(() => originalResult.Cast<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult, exception.ToString());
    }
}