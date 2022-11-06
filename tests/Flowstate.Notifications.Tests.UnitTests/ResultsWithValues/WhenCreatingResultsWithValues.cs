namespace Flowstate.Notifications.Tests.UnitTests.ResultsWithValues;

public class WhenCreatingResultsWithValues
{
    private readonly ErrorDetail _someErrorDetail = new("An Error");
    private readonly int? _someValue = 123;

    [Fact]
    public void EmptyDetailsInternalInstanceIsSameAsEmptyArrayOfErrorDetail() =>
        Assert.Same(Result<int?>.EmptyDetails, Array.Empty<ErrorDetail>());

    [Fact]
    public void UninitilizedStructHasExpectedMemberValues()
    {
        Result<int?> result = default;

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.Same(Result<int?>.EmptyDetails, result.Details);
    }

    [Fact]
    public void SuccessResultHasExpectedMemberValues()
    {
        var result = Result<int?>.Success(_someValue);

        Assert.True(result.Succeeded);
        Assert.Equal(_someValue, result.Value);
        Assert.Same(Result<int?>.EmptyDetails, result.Details);
    }

    [Fact]
    public void DeconstructionMapsExpectedVariables()
    {
        var result = Result<int?>.Success(_someValue);
        var (succeeded, value, details) = result;

        Assert.True(succeeded);
        Assert.Equal(_someValue, value);
        Assert.Same(Result<int?>.EmptyDetails, details);
    }

    [Fact]
    public void FailureResultWithoutDetailsHasExpectedMemberValues()
    {
        var result = Result<int?>.Failure();

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.Same(Result<int?>.EmptyDetails, result.Details);
    }

    [Fact]
    public void FailureResultWithDetailsHasExpectedMemberValues()
    {
        var result = Result<int?>.Failure(new[] { _someErrorDetail });

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }

    [Fact]
    public void FailureResultWithDetailsInitializedViaStringParamsArrayHasExpectedMemberValues()
    {
        var result = Result<int?>.Failure(_someErrorDetail.Description);

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }

    [Fact]
    public void FailureResultWithStringDetailsHasExpectedMemberValues()
    {
        var result = Result<int?>.Failure(_someErrorDetail.Description);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }


    [Fact]
    public void FailureResultWithUninitializedDetailsThrows()
    {
        var exception = Assert.Throws<ArgumentException>(() => Result<int?>.Failure(new ErrorDetail[] { default }));
        Assert.Contains(ResultsErrorMessages.DetailsContainsUninitializedItems, exception.ToString());
    }
}