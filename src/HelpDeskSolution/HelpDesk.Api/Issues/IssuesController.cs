using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Issues;

public class IssuesController : ControllerBase
{
    [HttpPost("/issues")]
    public async Task<ActionResult> AddAnIssueAsync(
       [FromBody] IssueCreateModel request
        )
    {
        return Ok(request);
    }
}


/*
{
    "software": "8398398",
    "description": "Thing broke"
}
*/

public record IssueCreateModel
{
    public string Software { get; init; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}