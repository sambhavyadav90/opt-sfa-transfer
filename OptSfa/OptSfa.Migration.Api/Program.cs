using OptSfa.Migration.Application.Interfaces;
using OptSfa.Migration.Application.Services;
using OptSfa.Migration.Data.Repository;
using OptSfa.Migration.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IClientRepository, ClientRepository>();


builder.Services.AddSingleton<IClientService, ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
