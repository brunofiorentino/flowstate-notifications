namespace Flowstate.Notifications.Tests.UnitTests.Results;

public class WhenCreatingResults
{
    private readonly ErrorDetail _anErrorDetail = new("An Error");

    [Fact]
    public void EmptyDetailsInternalInstanceIsSameAsEmptyArrayOfErrorDetail() =>
        Assert.Same(Array.Empty<ErrorDetail>(), Result.EmptyDetails);

    [Fact]
    public void UninitilizedStructHasExpectedMemberValues()
    {
        Result result = default;

        Assert.False(result.Succeeded);
        Assert.Same(Result.EmptyDetails, result.Details);
    }

    [Fact]
    public void SuccessResultHasExpectedMemberValues()
    {
        var result = Result.Success();

        Assert.True(result.Succeeded);
        Assert.Same(Result.EmptyDetails, result.Details);
    }

    [Fact]
    public void DeconstructionMapsExpectedVariables()
    {
        var result = Result.Success();
        var (succeeded, details) = result;

        Assert.True(succeeded);
        Assert.Same(Result.EmptyDetails, details);
    }

    [Fact]
    public void FailureResultWithDetailsHasExpectedMemberValues()
    {
        var result = Result.Failure(new[] { _anErrorDetail });

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_anErrorDetail, errorDetail);
    }

    [Fact]
    public void FailureResultWithUninitializedDetailsThrows()
    {
        var exception = Assert.Throws<ArgumentException>(() => Result.Failure(new ErrorDetail[] { default }));
        Assert.Contains(Result.DetailsContainsUninitializedItems, exception.ToString());
    }

    [Fact]
    public void FailureResultWithStringDetailsHasExpectedMemberValues()
    {
        var result = Result.Failure(_anErrorDetail.Description);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_anErrorDetail, errorDetail);
    }
}