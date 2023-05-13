namespace Flowstate.Notifications.Tests.UnitTests.FailureDetails;

public class WhenCreatingFailureDetails
{
    private static readonly string Description = "Some Error";
    private static readonly string Tag = "Some_Tag";

    [Fact]
    public void UninitializedStructHasExpectedMemberValues()
    {
        FailureDetail failureDetail = default;
        Assert.Null(failureDetail.Description);
        Assert.NotNull(failureDetail.Tags);
        Assert.Empty(failureDetail.Tags);
    }

    [Fact]
    public void FailureDetailWithTagsUnsetHasExpectedMemberValues()
    {
        var failureDetail = new FailureDetail(Description);
        Assert.Equal(Description, failureDetail.Description);
        Assert.Empty(failureDetail.Tags);
    }

    [Fact]
    public void FailureDetailWithTagsUnsetExplicitlyHasExpectedMemberValues()
    {
        var failureDetail = new FailureDetail(Description, null!);
        Assert.Empty(failureDetail.Tags);
    }

    [Fact]
    public void CannotCreateFailureDetailWithNullDescription() =>
        Assert.Throws<ArgumentNullException>(() => new FailureDetail(null!));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CannotCreateFailureDetailWithEmptyOrWhiteSpaceDescription(string nullOrEmptyDescription) =>
        Assert.Throws<ArgumentException>(() => new FailureDetail(nullOrEmptyDescription));


    [Fact]
    public void FailureDetailWithDescriptionAndTagsSetHasExpectedMemberValues()
    {
        var failureDetail = new FailureDetail(Description, Tag);
        Assert.Equal(Description, failureDetail.Description);
        Assert.Equal(Tag, failureDetail.Tags);
    }
}