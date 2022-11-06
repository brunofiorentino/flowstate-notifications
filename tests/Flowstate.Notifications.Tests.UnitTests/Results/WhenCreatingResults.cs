namespace Flowstate.Notifications.Tests.UnitTests.Results;

public class WhenCreatingResults
{
    private readonly ErrorDetail _someErrorDetail = new("An Error");

    [Fact] 
    public void Empty_details_internal_instance_is_same_as_platform_empty_array() =>
        Assert.Same(Array.Empty<ErrorDetail>(), Result.EmptyDetails);

    [Fact]
    public void Uninitilized_struct_has_expected_member_values()
    {
        Result result = default;

        Assert.False(result.Succeeded);
        Assert.Same(Result.EmptyDetails, result.Details);
    }

    [Fact]
    public void Success_result_has_expected_member_values()
    {
        var result = Result.Success();

        Assert.True(result.Succeeded);
        Assert.Same(Result.EmptyDetails, result.Details);
    }

    [Fact]
    public void Deconstruction_maps_expected_variables()
    {
        var result = Result.Success();
        var (succeeded, details) = result;

        Assert.True(succeeded);
        Assert.Same(Result.EmptyDetails, details);
    }

    [Fact]
    public void Failure_result_with_details_has_expected_member_values()
    {
        var result = Result.Failure(new[] { _someErrorDetail });

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }

    [Fact]
    public void Failure_result_with_details_initialized_via_string_params_array_has_expected_member_values()
    {
        const string error1 = "err1";
        const string error2 = "err2";
        var result = Result.Failure(error1, error2);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);
        Assert.Equal(2, result.Details.Count);
        Assert.Equal(error1, result.Details[0].Description);
        Assert.Equal(error2, result.Details[1].Description);
    }

    [Fact]
    public void Failure_result_with_uninitialized_details_throws()
    {
        var exception = Assert.Throws<ArgumentException>(() => Result.Failure(new ErrorDetail[] { default }));
        Assert.Contains(ResultsErrorMessages.DetailsContainsUninitializedItems, exception.ToString());
    }

    [Fact]
    public void Failure_result_with_string_details_has_expected_member_values()
    {
        var result = Result.Failure(_someErrorDetail.Description);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }
}