using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace HelpDesk.Api.Status;

public class StatusController(
    ILookupEmergencyContacts emergencyContactLookup
) : ControllerBase
{
    //private ILookupEmergencyContacts emergencyContactLookup;

    //public StatusController(ILookupEmergencyContacts emergencyContactLookup)
    //{
    //    this.emergencyContactLookup = emergencyContactLookup;
    //}


    [FeatureGate("Status")]
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
            EmergencyContact = await emergencyContactLookup.GetCurrentContactAsync()
        };
        return Ok(fakeResponse); // turn this into JSON (serializing) to send to the client.
    }
}
