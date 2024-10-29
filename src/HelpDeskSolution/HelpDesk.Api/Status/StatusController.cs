using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Status;

public class StatusController : ControllerBase
{
    [HttpGet("/status")]
    public async Task<ActionResult> GetTheStatus()
    {
        var fakeResponse = new StatusResponseModel
        {
            State = StatusState.Good,
            BacklogInfo = new Backlog
            {
                NumberOfIssuesInProcess = 99,
                NumberOfIssuesNotChecked = 22
            },
            EmergencyContact = new EmergencyContactInfo
            {
                Name = "Jeff",
                EmailAddress = "jeff@company.com",
                PhoneNumber = "555-1212"
            }
        };
        return Ok(fakeResponse); // turn this into JSON (serializing) to send to the client.
    }
}
