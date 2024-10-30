namespace OnCall.Api.HelpDesk;

public static class Api
{
    public static IServiceCollection UseHelpDeskApi(this IServiceCollection services)
    {
        // we can set up our specific services here.
        services.AddScoped<IProvideTheBusinessClock, BusinessClock>();
        return services;
    }
    public static WebApplication MapHelpDeskApi(this WebApplication app)
    {

        app.MapGet("/on-call", GetOnCall);
        return app;
    }

    public static async Task<IResult> GetOnCall(IProvideTheBusinessClock clock)
    {
        var now = DateTimeOffset.Now;
        if (await clock.CurrentlyOpen())
        {

            var response = new EmergencyContactInfo()
            {
                Name = "Bob Smith",
                PhoneNumber = "555-1212",
                EmailAddress = "bob@company.com"
            };
            return Results.Ok(response);
        }
        else
        {
            var response = new EmergencyContactInfo()
            {
                Name = "Support Professionals",
                PhoneNumber = "888 GET-LOST",
                EmailAddress = "help@fake-support.com"
            };
            return Results.Ok(response);
        }
    }
}

public class BusinessClock(TimeProvider clock) : IProvideTheBusinessClock
{
    public async Task<bool> CurrentlyOpen()
    {
        var now = clock.GetLocalNow();
        return now.Hour is >= 8 and < 17 && now.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday;
    }
}


public interface IProvideTheBusinessClock
{
    Task<bool> CurrentlyOpen();
}