namespace Flowstate.Notifications.Tests.UnitTests.FailureDetails;

public class WhenComparingFailureDetails
{
    [Fact]
    public void Generates_ValueObject_hash_codes()
    {
        var a1Tuple = ("ad", "at");
        var a1 = new FailureDetail(a1Tuple.Item1, a1Tuple.Item2);
        var a2 = new FailureDetail(a1Tuple.Item1, a1Tuple.Item2);

        var b1Tuple = ("bd", "bt");
        var b1 = new FailureDetail(b1Tuple.Item1, b1Tuple.Item2);
        var b2 = new FailureDetail(b1Tuple.Item1, b1Tuple.Item2);

        FailureDetail c1 = default;
        FailureDetail c2 = default;

        Assert.Equal(a1Tuple.GetHashCode(), a1.GetHashCode());
        Assert.Equal(a1.GetHashCode(), a2.GetHashCode());
        Assert.NotEqual(a1.GetHashCode(), b1.GetHashCode());

        Assert.Equal(b1Tuple.GetHashCode(), b1.GetHashCode());
        Assert.Equal(b1.GetHashCode(), b2.GetHashCode());

        Assert.Equal(0, c1.GetHashCode());
        Assert.Equal(0, c2.GetHashCode());
    }

    [Fact]
    public void Compares_ValueObject_equality_for_all_members()
    {
        var a = new FailureDetail("ad", "at");
        var b = new FailureDetail("bd", "bt");

        Assert.True(a.Equals(a));
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Compares_ValueObject_equality_considering_unset_tag_member()
    {
        var a = new FailureDetail("ad");
        var b = new FailureDetail("bd");

        Assert.True(a.Equals(a));
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Compares_ValueObject_equality_for_uninitialized_structs()
    {
        // Depends on IEquatable implementation otherwise boxing will break uninitilized/default comparisons

        FailureDetail a = default;
        FailureDetail b = default;
        var c = new FailureDetail("cd");

        Assert.True(a.Equals(b));
        Assert.False(a.Equals(c));
    }
}