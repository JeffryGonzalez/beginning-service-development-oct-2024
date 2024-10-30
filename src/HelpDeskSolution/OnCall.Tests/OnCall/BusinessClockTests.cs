

using Microsoft.Extensions.Time.Testing;
using OnCall.Api.HelpDesk;

namespace OnCall.Tests.OnCall;
[Trait("Category", "Unit")]
public class BusinessClockTests
{
    [Theory]
    [MemberData(nameof(GetClosedExamples))]
    public async Task ExamplesOfWhenWeAreClosed(DateTimeOffset example)
    {
        var faketime = new FakeTimeProvider(example);

        var clock = new BusinessClock(faketime);

        Assert.False(await clock.CurrentlyOpen());
    }
    [Theory]
    [MemberData(nameof(GetOpenExamples))]
    public async Task ExamplesOfWhenWeAreOpen(DateTimeOffset example)
    {
        var faketime = new FakeTimeProvider(example);

        var clock = new BusinessClock(faketime);


        Assert.True(await clock.CurrentlyOpen());
    }

    public static IEnumerable<object[]> GetClosedExamples()
    {
        yield return new object[] { new DateTimeOffset(2024, 10, 30, 7, 59, 59, TimeSpan.FromHours(-4)) };
        yield return new object[] { new DateTimeOffset(2024, 10, 30, 17, 00, 01, TimeSpan.FromHours(-4)) };
        yield return new object[] { new DateTimeOffset(2024, 11, 2, 10, 00, 01, TimeSpan.FromHours(-4)) };
        yield return new object[] { new DateTimeOffset(2024, 11, 3, 10, 00, 01, TimeSpan.FromHours(-4)) };
    }

    public static IEnumerable<object[]> GetOpenExamples()
    {
        yield return new object[] { new DateTimeOffset(2024, 10, 30, 8, 0, 0, TimeSpan.FromHours(-4)) };
        yield return new object[] { new DateTimeOffset(2024, 10, 30, 16, 59, 59, TimeSpan.FromHours(-4)) };

    }
}
