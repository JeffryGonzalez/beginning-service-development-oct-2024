
using Marten;

namespace HelpDesk.Api.Issues.Services;

public class UserIssuesManager(IDocumentSession session)
{
    public async Task<IssueResponseModel> AddUserIssueAsync(Guid softwareId, IssueCreateModel request)
    {
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
        return response;
    }

    public async Task<IssueResponseModel?> GetIssueByIdAsync(Guid softwareId, Guid issueId)
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
        return savedIssue;
    }
}
