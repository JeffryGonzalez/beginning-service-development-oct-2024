namespace HelpDesk.Api.Status;

/*
Status: Good | Degraded | Failing 

Backlog: {
    numberOfIssuesNotChecked: 99,
    numberofIssuesInProcess: 22,
}

emergencyContact: {
    name: "Jennifer",
    email: j@aol.com,
    phone: 555-1212
}*/

public enum StatusState { Good, Degraded, Failing }
public record Backlog
{
    public int NumberOfIssuesNotChecked { get; init; }
    public int NumberOfIssuesInProcess { get; init; }
}

public record EmergencyContactInfo
{
    public string Name { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}

public record StatusResponseModel
{
    public StatusState State { get; init; }
    public Backlog BacklogInfo { get; init; } = new();
    public EmergencyContactInfo EmergencyContact { get; init; } = new();
}