namespace HelpDesk.Api.Issues;
public record IssueCreateModel
{

    public string Description { get; set; } = string.Empty;
}

public record IssueResponseModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public IssueStatus Status { get; set; }
    public Guid SoftwareId { get; set; }
}