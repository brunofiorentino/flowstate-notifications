namespace Flowstate.Notifications.Tests.UnitTests.Results;

public class WhenCastingResults
{
    [Fact]
    public void SuccessResult_implicit_boolean_cast_produces_expected_value()
    {
        var result = Result.Success();
        Assert.True(result);
    }

    [Fact]
    public void FailureResult_implicit_boolean_cast_produces_expected_value()
    {
        var result = Result.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureResult_with_value_cast_produces_expected_value()
    {
        var originalResult = Result.Failure("err1", "err2");
        var newResult = originalResult.AsValuedFailure<long?>();

        Assert.False(newResult.Succeeded);
        Assert.Equal(originalResult.FailureDetails.Count, newResult.FailureDetails.Count);

        Assert.All(
            originalResult.FailureDetails.Zip(newResult.FailureDetails),
            originalNewPair => Assert.Equal(originalNewPair.First, originalNewPair.Second));
    }

    [Fact]
    public void Trying_to_force_FailureResult_cast_on_SuccessResult_throws()
    {
        var originalResult = Result.Success();
        var exception = Assert.Throws<Exception>(() => originalResult.AsValuedFailure<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult, exception.ToString());
    }
}