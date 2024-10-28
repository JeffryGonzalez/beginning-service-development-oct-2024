var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
