namespace Flowstate.Notifications.Tests.UnitTests.ErrorDetails;

public class WhenCreatingErrorDetails
{
    private readonly string Description = "Some Error";
    private readonly string Tag = "Some_Tag";

    [Fact]
    public void UninitilizedStructHasExpectedMemberValues()
    {
        ErrorDetail errorDetail = default;
        Assert.Null(errorDetail.Description);
        Assert.NotNull(errorDetail.Tag);
        Assert.Empty(errorDetail.Tag);
    }

    [Fact]
    public void ErrorDetailWithTagUnsetHasExpectedMemberValues()
    {
        var errorDetail = new ErrorDetail(Description);
        Assert.Equal(Description, errorDetail.Description);
        Assert.Empty(errorDetail.Tag);
    }

    [Fact]
    public void ErrorDetailWithTagUnsetExplicitlyHasExpectedMemberValues()
    {
        var errorDetail = new ErrorDetail(Description, null!);
        Assert.Empty(errorDetail.Tag);
    }

    [Fact]
    public void CannotCreateErrorDetailWithNullDescription() =>
        Assert.Throws<ArgumentNullException>(() => new ErrorDetail(null!));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CannotCreateErrorDetailWithEmptyOrWhiteSpaceDescription(string nullOrEmptyDescription) =>
        Assert.Throws<ArgumentException>(() => new ErrorDetail(nullOrEmptyDescription));


    [Fact]
    public void ErrorDetailWithDescriptionAndTagSetHasExpectedMemberValues()
    {
        var errorDetail = new ErrorDetail(Description, Tag);
        Assert.Equal(Description, errorDetail.Description);
        Assert.Equal(Tag, errorDetail.Tag);
    }
}