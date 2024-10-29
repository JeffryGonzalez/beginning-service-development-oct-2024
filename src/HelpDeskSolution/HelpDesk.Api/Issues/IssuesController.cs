using Marten;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Api.Issues;

public class IssuesController(IDocumentSession session) : ControllerBase
{
    [HttpPost("/user/software/{softwareId:guid}/issues")]

    public async Task<ActionResult> AddAnIssueAsync(
       [FromBody] IssueCreateModel request,
       [FromRoute] Guid softwareId
        )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // Rules here - You have to authenticated (only employees can do this)
        // You have to give us a piece of software we support
        // You have to give us a description of that software (10-1024)
        // Bad Request - 400.

        // What are we going to write to the database (entity)
        // what are we going to return to the user.

        var entity = new IssueEntity
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            SoftwareId = softwareId,
            Status = IssueStatus.Pending,
            SubmittedBy = "bob-smith",
            SubmittedDate = DateTimeOffset.Now,

        };

        session.Store(entity);
        await session.SaveChangesAsync();

        var response = new IssueResponseModel
        {
            Id = entity.Id,
            Description = request.Description,
            SoftwareId = entity.SoftwareId,
            Status = entity.Status,


        };
        return Ok(response);
    }

    [HttpGet("/user/software/{softwareId:guid}/issues/{issueId:guid}")]
    public async Task<ActionResult> GetIssueById(
        [FromRoute] Guid softwareId, [FromRoute] Guid issueId)
    {
        var savedIssue = await session.Query<IssueEntity>().Where(i => i.Id == issueId && i.SoftwareId == softwareId)
            .Select(i => new IssueResponseModel
            {
                Id = i.Id,
                Description = i.Description,
                SoftwareId = i.SoftwareId,
                Status = i.Status,

            })
            .SingleOrDefaultAsync();

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


/*
{
    "software": "8398398",
    "description": "Thing broke"
}
*/

public record IssueCreateModel
{
    [Required, MinLength(10), MaxLength(1024)]
    public string Description { get; set; } = string.Empty;
}

public record IssueResponseModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public IssueStatus Status { get; set; }
    public Guid SoftwareId { get; set; }
}


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