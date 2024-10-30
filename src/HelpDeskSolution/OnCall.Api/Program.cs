using OnCall.Api.HelpDesk;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(p => TimeProvider.System);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.UseHelpDeskApi();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHelpDeskApi();

app.Run();

public partial class Program { }