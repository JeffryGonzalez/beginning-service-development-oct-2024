using HelpDesk.Api.Issues.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Issues;

public class UserIssuesController(UserIssuesManager manager) : ControllerBase
{
    [HttpPost("/user/software/{softwareId:guid}/issues")]

    public async Task<ActionResult> AddAnIssueAsync(
       [FromBody] IssueCreateModel request,
       [FromRoute] Guid softwareId,
       [FromServices] IssueCreateModelValidations validator
        )
    {
        // check the software by doing an http call to another persons api
        var validations = await validator.ValidateAsync(request);

        if (!validations.IsValid)
        {
            return BadRequest();
        }

        IssueResponseModel response = await manager.AddUserIssueAsync(softwareId, request);

        return Ok(response);
    }

    [HttpGet("/user/software/{softwareId:guid}/issues/{issueId:guid}")]
    public async Task<ActionResult> GetIssueById(
        [FromRoute] Guid softwareId, [FromRoute] Guid issueId)
    {
        IssueResponseModel? savedIssue = await manager.GetIssueByIdAsync(softwareId, issueId);


        if (savedIssue != null)
        {
            return Ok(savedIssue);

        }
        else
        {
            return NotFound();
        }
    }
}








