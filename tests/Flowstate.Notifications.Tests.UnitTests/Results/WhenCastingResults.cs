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
}