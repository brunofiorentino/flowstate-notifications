namespace Flowstate.Notifications.Tests.UnitTests.ValuedResults;

public class WhenCastingValuedResults
{
    private readonly int? _someValue = 123;

    [Fact]
    public void Success_result_to_boolean_implicit_cast_produces_expected_value()
    {
        var result = Result<int?>.Success(_someValue);
        Assert.True(result);
    }

    [Fact]
    public void Failure_result_to_boolean_implicit_cast_produces_expected_value()
    {
        var result = Result<int?>.Failure();
        Assert.False(result);
    }

    [Fact]
    public void Failure_result_to_other_valued_result_cast_produces_expected_value()
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
    public void Success_result_to_valueless_result_cast_produces_expected_value()
    {
        var originalResult = Result<int?>.Success(_someValue);
        var newResult = originalResult.Cast();

        Assert.True(newResult.Succeeded);
        Assert.Empty(newResult.FailureDetails);
    }

    [Fact]
    public void Failure_result_to_valueless_result_cast_produces_expected_value()
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
    public void Trying_to_force_Success_result_to_Failure_result_cast_throws()
    {
        var originalResult = Result<int?>.Success(123);
        var exception = Assert.Throws<Exception>(() => originalResult.Cast<long?>());
        Assert.Contains(ResultsErrorMessages.CannotCastSuccessResultAsFailureResult, exception.ToString());
    }
}