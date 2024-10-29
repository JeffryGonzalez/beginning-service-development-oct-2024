namespace HelpDesk.Api.Issues;

public enum IssueStatus { Pending }
public class IssueEntity
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public Guid SoftwareId { get; set; }

    public string SubmittedBy { get; set; } = string.Empty;

    public DateTimeOffset SubmittedDate { get; set; }

    public IssueStatus Status { get; set; }

}