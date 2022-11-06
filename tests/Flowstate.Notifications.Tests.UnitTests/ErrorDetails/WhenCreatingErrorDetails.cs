namespace Flowstate.Notifications.Tests.UnitTests.ErrorDetails;

public class WhenCreatingErrorDetails
{
    private readonly string Description = "Some Error";
    private readonly string Tag = "Some_Tag";

    [Fact]
    public void Uninitilized_struct_has_expected_member_values()
    {
        ErrorDetail errorDetail = default;
        Assert.Null(errorDetail.Description);
        Assert.NotNull(errorDetail.Tag);
        Assert.Empty(errorDetail.Tag);
    }

    [Fact]
    public void ErrorDetail_with_tag_unset_has_expected_member_values()
    {
        var errorDetail = new ErrorDetail(Description);
        Assert.Equal(Description, errorDetail.Description);
        Assert.Empty(errorDetail.Tag);
    }

    [Fact]
    public void ErrorDetail_with_tag_unset_explicitly_has_expected_member_values()
    {
        var errorDetail = new ErrorDetail(Description, null!);
        Assert.Empty(errorDetail.Tag);
    }

    [Fact]
    public void Cannot_create_error_detail_with_null_description() =>
        Assert.Throws<ArgumentNullException>(() => new ErrorDetail(null!));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Cannot_create_error_detail_with_empty_or_white_space_description(string nullOrEmptyDescription) =>
        Assert.Throws<ArgumentException>(() => new ErrorDetail(nullOrEmptyDescription));


    [Fact]
    public void ErrorDetail_with_description_and_tag_set_has_expected_member_values()
    {
        var errorDetail = new ErrorDetail(Description, Tag);
        Assert.Equal(Description, errorDetail.Description);
        Assert.Equal(Tag, errorDetail.Tag);
    }
}