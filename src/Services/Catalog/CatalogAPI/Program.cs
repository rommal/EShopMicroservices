using Microsoft.CodeAnalysis.CSharp.Syntax;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(ops =>
{
    string connectionString = builder.Configuration.GetConnectionString("Database") ?? string.Empty;

    ops.Connection(connectionString);

}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
