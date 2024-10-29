using FluentValidation.TestHelper;
using HelpDesk.Api.Issues;

namespace HelpDesk.Tests.Status;
public class ModelValidations
{
    [Theory]
    [InlineData("This is a good description")]
    [InlineData("One More Time")]
    [Trait("Category", "UnitTest")]
    public void GoodExamples(string description)
    {

        var validator = new IssueCreateModelValidations();

        var goodModel = new IssueCreateModel { Description = description };

        var result = validator.TestValidate(goodModel);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("123456789")]
    [InlineData(null)]
    [Trait("Category", "UnitTest")]
    public void BadExamples(string description)
    {

        var validator = new IssueCreateModelValidations();

        var goodModel = new IssueCreateModel { Description = description };

        var result = validator.TestValidate(goodModel);

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(e => e.Description);
    }

    [Fact]
    public void BadExampleLongDescription()
    {
        var description = new string('X', 1025);
        var validator = new IssueCreateModelValidations();

        var goodModel = new IssueCreateModel { Description = description };

        var result = validator.TestValidate(goodModel);

        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(e => e.Description);
    }
}
