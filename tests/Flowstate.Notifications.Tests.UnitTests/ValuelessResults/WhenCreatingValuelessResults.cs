namespace Flowstate.Notifications.Tests.UnitTests.ValuelessResults;

public class WhenCreatingValuelessResults
{
    private readonly FailureDetail _someFailureDetail = new("An Error");

    [Fact]
    public void EmptyFailureDetailsInternalInstanceIsSameAsPlatformArrayEmpty() =>
        Assert.Same(Array.Empty<FailureDetail>(), Result.EmptyFailureDetails);

    [Fact]
    public void UninitializedStructHasExpectedMemberValues()
    {
        Result result = default;

        Assert.False(result.Succeeded);
        Assert.Same(Result.EmptyFailureDetails, result.FailureDetails);
    }

    [Fact]
    public void SuccessResultHasExpectedMemberValues()
    {
        var result = Result.Success();

        Assert.True(result.Succeeded);
        Assert.Same(Result.EmptyFailureDetails, result.FailureDetails);
    }

    [Fact]
    public void DeconstructionMapsExpectedVariables()
    {
        var result = Result.Success();
        var (succeeded, failureDetails) = result;

        Assert.True(succeeded);
        Assert.Same(Result.EmptyFailureDetails, failureDetails);
    }

    [Fact]
    public void FailureResultWithDetailsHasExpectedMemberValues()
    {
        var result = Result.Failure(new[] { _someFailureDetail });

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.FailureDetails);

        var failureDetail = result.FailureDetails.Single();
        Assert.Equal(_someFailureDetail, failureDetail);
    }

    [Fact]
    public void FailureResultWithDetailsInitializedViaStringParamsArrayHasExpectedMemberValues()
    {
        const string error1 = "err1";
        const string error2 = "err2";
        var result = Result.Failure(error1, error2);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.FailureDetails);
        Assert.Equal(2, result.FailureDetails.Count);
        Assert.Equal(error1, result.FailureDetails[0].Description);
        Assert.Equal(error2, result.FailureDetails[1].Description);
    }

    [Fact]
    public void FailureResultWithUninitializedDetailsThrows()
    {
        var exception = Assert.Throws<ArgumentException>(() => Result.Failure(new FailureDetail[] { default }));
        Assert.Contains(ResultsErrorMessages.DetailsContainsUninitializedItems, exception.ToString());
    }

    [Fact]
    public void FailureResultWithStringDetailsHasExpectedMemberValues()
    {
        var result = Result.Failure(_someFailureDetail.Description);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.FailureDetails);

        var failureDetail = result.FailureDetails.Single();
        Assert.Equal(_someFailureDetail, failureDetail);
    }
}