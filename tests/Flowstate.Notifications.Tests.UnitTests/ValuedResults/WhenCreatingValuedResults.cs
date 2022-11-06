namespace Flowstate.Notifications.Tests.UnitTests.ValuedResults;

public class WhenCreatingValuedResults
{
    private readonly FailureDetail _someFailureDetail = new("An Error");
    private readonly int? _someValue = 123;

    [Fact]
    public void EmptyFailureDetails_internal_instance_is_same_as_platform_ArrayEmpty() =>
        Assert.Same(Result<int?>.EmptyFailureDetails, Array.Empty<FailureDetail>());

    [Fact]
    public void Uninitilized_struct_has_expected_member_values()
    {
        Result<int?> result = default;

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.Same(Result<int?>.EmptyFailureDetails, result.FailureDetails);
    }

    [Fact]
    public void SuccessResult_has_expected_member_values()
    {
        var result = Result<int?>.Success(_someValue);

        Assert.True(result.Succeeded);
        Assert.Equal(_someValue, result.Value);
        Assert.Same(Result<int?>.EmptyFailureDetails, result.FailureDetails);
    }

    [Fact]
    public void Deconstruction_maps_expected_variables()
    {
        var result = Result<int?>.Success(_someValue);
        var (succeeded, value, failureDetails) = result;

        Assert.True(succeeded);
        Assert.Equal(_someValue, value);
        Assert.Same(Result<int?>.EmptyFailureDetails, failureDetails);
    }

    [Fact]
    public void FailureResult_without_details_has_expected_member_values()
    {
        var result = Result<int?>.Failure();

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.Same(Result<int?>.EmptyFailureDetails, result.FailureDetails);
    }

    [Fact]
    public void FailureResult_with_details_has_expected_member_values()
    {
        var result = Result<int?>.Failure(new[] { _someFailureDetail });

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.NotEmpty(result.FailureDetails);

        var failureDetail = result.FailureDetails.Single();
        Assert.Equal(_someFailureDetail, failureDetail);
    }

    [Fact]
    public void FailureResult_with_details_initialized_via_string_params_array_has_expected_member_values()
    {
        const string error1 = "err1";
        const string error2 = "err2";
        var result = Result<int?>.Failure(error1, error2);

        Assert.False(result.Succeeded);
        Assert.Equal(default, result.Value);
        Assert.NotEmpty(result.FailureDetails);
        Assert.Equal(2, result.FailureDetails.Count);
        Assert.Equal(error1, result.FailureDetails[0].Description);
        Assert.Equal(error2, result.FailureDetails[1].Description);
    }

    [Fact]
    public void FailureResult_with_string_details_has_expected_member_values()
    {
        var result = Result<int?>.Failure(_someFailureDetail.Description);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.FailureDetails);

        var failureDetail = result.FailureDetails.Single();
        Assert.Equal(_someFailureDetail, failureDetail);
    }


    [Fact]
    public void FailureResult_with_uninitialized_details_throws()
    {
        var exception = Assert.Throws<ArgumentException>(() => Result<int?>.Failure(new FailureDetail[] { default }));
        Assert.Contains(ResultsErrorMessages.DetailsContainsUninitializedItems, exception.ToString());
    }
}