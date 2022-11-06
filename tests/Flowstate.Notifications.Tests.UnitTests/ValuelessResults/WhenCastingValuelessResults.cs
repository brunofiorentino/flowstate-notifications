namespace Flowstate.Notifications.Tests.UnitTests.ValuelessResults;

public class WhenCastingValuelessResults
{
    [Fact]
    public void SuccessResult_to_boolean_implicit_cast_produces_expected_value()
    {
        var result = Result.Success();
        Assert.True(result);
    }

    [Fact]
    public void FailureResult_to_boolean_implicit_cast_produces_expected_value()
    {
        var result = Result.Failure();
        Assert.False(result);
    }

    [Fact]
    public void FailureResult_to_valued_cast_produces_expected_value()
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
    public void Trying_to_force_SuccessResult_to_FailureResult_cast_throws()
    {
        var originalResult = Result.Success();
        var exception = Assert.Throws<Exception>(() => originalResult.Cast<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult, exception.ToString());
    }
}