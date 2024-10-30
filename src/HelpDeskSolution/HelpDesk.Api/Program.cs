using FluentValidation;
using HelpDesk.Api.Issues;
using HelpDesk.Api.Issues.Services;
using HelpDesk.Api.Status;
using HelpDesk.Api.Status.Services;
using Marten;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFeatureManagement();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hey API, if anything you create needs an ILookupEmergencyContacts, use DummyEmergencyLookupThing
var connectionString = builder.Configuration.GetConnectionString("issues") ?? throw new Exception("Don't start this API, there is no connection strings");
Console.WriteLine(connectionString);
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions();

var onCallApiUrl = builder.Configuration.GetValue<string>("onCallAddress") ?? throw new Exception("No URL for on Call Api");

builder.Services.AddHttpClient<OnCallApiClient>(client =>
{
    client.BaseAddress = new Uri(onCallApiUrl);
});

//builder.Services.AddScoped<ILookupEmergencyContacts, OnCallApiClient>();
builder.Services.AddScoped<ILookupEmergencyContacts>(sp =>
{
    return sp.GetRequiredService<OnCallApiClient>();
});
builder.Services.AddScoped<UserIssuesManager>();
builder.Services.AddValidatorsFromAssemblyContaining<IssueCreateModelValidations>();
builder.Services.AddFluentValidationRulesToSwagger();
// above here (above the builder.Build()) is the behind the scenes configuration of the services our API has.
var app = builder.Build();
// after this is configuring how HTTP requests are handled and responses are created.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers(); // reflection portion - create a "routing table"



app.Run(); // this is where the application starts running. It is a big while(true) { }

public partial class Program { }
