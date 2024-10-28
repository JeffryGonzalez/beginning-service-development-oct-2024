namespace HelpDesk.Tests;

public class UnitTest1
{
    [Fact]
    public void CanAddTenAndTwenty()
    {
        // "Given" (arrange the context of the test)
        int a = 10; int b = 20; int answer;

        // "When" - the actual test
        answer = a + b; // System Under Test

        // "Then" - the assertion. 
        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(2, 2, 4)]
    [InlineData(10, 3, 13)]
    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        var answer = a + b;

        Assert.Equal(expected, answer);
    }

}