namespace Flowstate.Notifications.Tests.UnitTests.FailureDetails;

public class WhenCreatingFailureDetails
{
    private readonly string Description = "Some Error";
    private readonly string Tag = "Some_Tag";

    [Fact]
    public void Uninitilized_struct_has_expected_member_values()
    {
        FailureDetail failureDetail = default;
        Assert.Null(failureDetail.Description);
        Assert.NotNull(failureDetail.Tags);
        Assert.Empty(failureDetail.Tags);
    }

    [Fact]
    public void FailureDetail_with_tags_unset_has_expected_member_values()
    {
        var failureDetail = new FailureDetail(Description);
        Assert.Equal(Description, failureDetail.Description);
        Assert.Empty(failureDetail.Tags);
    }

    [Fact]
    public void FailureDetail_with_tags_unset_explicitly_has_expected_member_values()
    {
        var failureDetail = new FailureDetail(Description, null!);
        Assert.Empty(failureDetail.Tags);
    }

    [Fact]
    public void Cannot_create_FailureDetail_with_null_description() =>
        Assert.Throws<ArgumentNullException>(() => new FailureDetail(null!));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Cannot_create_FailureDetail_with_empty_or_white_space_description(string nullOrEmptyDescription) =>
        Assert.Throws<ArgumentException>(() => new FailureDetail(nullOrEmptyDescription));


    [Fact]
    public void FailureDetail_with_description_and_tags_set_has_expected_member_values()
    {
        var failureDetail = new FailureDetail(Description, Tag);
        Assert.Equal(Description, failureDetail.Description);
        Assert.Equal(Tag, failureDetail.Tags);
    }
}