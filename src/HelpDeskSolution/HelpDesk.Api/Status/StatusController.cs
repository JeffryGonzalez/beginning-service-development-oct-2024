using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Status;

public class StatusController : ControllerBase
{
    [HttpGet("/status")]
    public async Task<ActionResult> GetTheStatus()
    {
        return Ok();
    }
}
