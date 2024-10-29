using FluentValidation;

namespace HelpDesk.Api.Issues;

public class IssueCreateModelValidations : AbstractValidator<IssueCreateModel>
{
    public IssueCreateModelValidations()
    {
        RuleFor(i => i.Description).NotEmpty();
        RuleFor(i => i.Description).MinimumLength(10);
        RuleFor(i => i.Description).MaximumLength(1024);
    }
}
