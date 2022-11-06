namespace Flowstate.Notifications.Tests.UnitTests.Results;

public class WhenCastingResults
{
    [Fact]
    public void SuccessImplicitBooleanCastProducesExpectedValue()
    {
        var result = Result.Success();
        Assert.True(result);
    }

    [Fact]
    public void FailureImplicitBooleanCastProducesExpectedValue()
    {
        var result = Result.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureWithValueCastProducesExpectedValue()
    {
        var originalResult = Result.Failure("err1", "err2");
        var newResult = originalResult.CastFailure<long?>();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.Details.Count, newResult.Details.Count);

        Assert.All(
            originalResult.Details.Zip(newResult.Details),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void TryingToForceFailureCastOnSuccessResultThrows()
    {
        var originalResult = Result.Success();
        var exception = Assert.Throws<Exception>(() => originalResult.CastFailure<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSucceededResultAsFailure, exception.ToString());
    }
}