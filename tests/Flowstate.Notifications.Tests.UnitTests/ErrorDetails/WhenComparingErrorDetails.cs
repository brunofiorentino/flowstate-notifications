namespace Flowstate.Notifications.Tests.UnitTests.ErrorDetails;

public class WhenComparingErrorDetails
{
    [Fact]
    public void HashCode_expectations_are_met()
    {
        var a1Tuple = ("ad", "at");
        var a1 = new ErrorDetail(a1Tuple.Item1, a1Tuple.Item2);
        var a2 = new ErrorDetail(a1Tuple.Item1, a1Tuple.Item2);

        var b1Tuple = ("bd", "bt");
        var b1 = new ErrorDetail(b1Tuple.Item1, b1Tuple.Item2);
        var b2 = new ErrorDetail(b1Tuple.Item1, b1Tuple.Item2);

        ErrorDetail c1 = default;
        ErrorDetail c2 = default;

        Assert.Equal(a1Tuple.GetHashCode(), a1.GetHashCode());
        Assert.Equal(a1.GetHashCode(), a2.GetHashCode());
        Assert.NotEqual(a1.GetHashCode(), b1.GetHashCode());

        Assert.Equal(b1Tuple.GetHashCode(), b1.GetHashCode());
        Assert.Equal(b1.GetHashCode(), b2.GetHashCode());

        Assert.Equal(0, c1.GetHashCode());
        Assert.Equal(0, c2.GetHashCode());
    }

    [Fact]
    public void Equality_comparisons_with_all_members_set_are_precise()
    {
        var a = new ErrorDetail("ad", "at");
        var b = new ErrorDetail("bd", "bt");

        Assert.True(a.Equals(a));
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equality_comparisons_with_tag_member_unset_are_precise()
    {
        var a = new ErrorDetail("ad");
        var b = new ErrorDetail("bd");

        Assert.True(a.Equals(a));
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equality_comparisons_for_unintialized_structs_are_precise()
    {
        // Depends on IEquatable implementation otherwise boxing will break uninitilized/default comparisons

        ErrorDetail a = default;
        ErrorDetail b = default;
        var c = new ErrorDetail("cd");

        Assert.True(a.Equals(b));
        Assert.False(a.Equals(c));
    }
}