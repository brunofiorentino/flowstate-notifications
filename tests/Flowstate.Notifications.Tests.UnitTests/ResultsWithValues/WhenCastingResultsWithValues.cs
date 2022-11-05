namespace Flowstate.Notifications.Tests.UnitTests.ResultsWithValues;

public class WhenCastingResultsWithValues
{
    private readonly int? _someValue = 123;

    [Fact]
    public void SuccessImplicitBooleanCastProducesExpectedValue()
    {
        var result = Result<int?>.Success(_someValue);
        Assert.True(result);
    }

    [Fact]
    public void FailureImplicitBooleanCastProducesExpectedValue()
    {
        var result = Result<int?>.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureWithValueCastProducesExpectedValue()
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
    public void FailureWithoutValueCastProducesExpectedValue()
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
    public void TryingToForceFailureCastOnSuccessResultThrows()
    {
        var originalResult = Result<int?>.Success(123);
        var exception = Assert.Throws<Exception>(() => originalResult.CastFailure<long?>());
        Assert.Contains(Result<int?>.CannotCastSucceededResultAsFailure, exception.ToString());
    }
}