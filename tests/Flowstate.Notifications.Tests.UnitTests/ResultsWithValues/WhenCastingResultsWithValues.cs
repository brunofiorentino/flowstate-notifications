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
}