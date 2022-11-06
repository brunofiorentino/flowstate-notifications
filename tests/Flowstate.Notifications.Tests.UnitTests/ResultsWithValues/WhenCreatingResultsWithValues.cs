namespace Flowstate.Notifications.Tests.UnitTests.ResultsWithValues;

public class WhenCreatingResultsWithValues
{
    private readonly ErrorDetail _someErrorDetail = new("An Error");
    private readonly int? _someValue = 123;

    [Fact]
    public void Empty_details_internal_instance_is_same_as_platform_empty_array() =>
        Assert.Same(Result<int?>.EmptyDetails, Array.Empty<ErrorDetail>());

    [Fact]
    public void Uninitilized_struct_has_expected_member_values()
    {
        Result<int?> result = default;

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.Same(Result<int?>.EmptyDetails, result.Details);
    }

    [Fact]
    public void Success_result_has_expected_member_values()
    {
        var result = Result<int?>.Success(_someValue);

        Assert.True(result.Succeeded);
        Assert.Equal(_someValue, result.Value);
        Assert.Same(Result<int?>.EmptyDetails, result.Details);
    }

    [Fact]
    public void Deconstruction_maps_expected_variables()
    {
        var result = Result<int?>.Success(_someValue);
        var (succeeded, value, details) = result;

        Assert.True(succeeded);
        Assert.Equal(_someValue, value);
        Assert.Same(Result<int?>.EmptyDetails, details);
    }

    [Fact]
    public void Failure_result_without_details_has_expected_member_values()
    {
        var result = Result<int?>.Failure();

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.Same(Result<int?>.EmptyDetails, result.Details);
    }

    [Fact]
    public void Failure_result_with_details_has_expected_member_values()
    {
        var result = Result<int?>.Failure(new[] { _someErrorDetail });

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }

    [Fact]
    public void Failure_result_with_details_initialized_via_string_params_array_has_expected_member_values()
    {
        const string error1 = "err1";
        const string error2 = "err2";
        var result = Result<int?>.Failure(error1, error2);

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.NotEmpty(result.Details);
        Assert.Equal(2, result.Details.Count);
        Assert.Equal(error1, result.Details[0].Description);
        Assert.Equal(error2, result.Details[1].Description);
    }

    [Fact]
    public void Failure_result_with_string_details_has_expected_member_values()
    {
        var result = Result<int?>.Failure(_someErrorDetail.Description);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Details);

        var errorDetail = result.Details.Single();
        Assert.Equal(_someErrorDetail, errorDetail);
    }


    [Fact]
    public void Failure_result_with_uninitialized_details_throws()
    {
        var exception = Assert.Throws<ArgumentException>(() => Result<int?>.Failure(new ErrorDetail[] { default }));
        Assert.Contains(ResultsErrorMessages.DetailsContainsUninitializedItems, exception.ToString());
    }
}