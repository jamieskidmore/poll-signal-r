using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using PollSignalR.Models;
using Microsoft.AspNetCore.OpenApi;
using PollSignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

var PGHOST = Environment.GetEnvironmentVariable("PGHOST");
var PGDATABASE = Environment.GetEnvironmentVariable("PGDATABASE");
var PGUSER = Environment.GetEnvironmentVariable("PGUSER");
var PGPASSWORD = Environment.GetEnvironmentVariable("PGPASSWORD");
var connectionString = $"Host={PGHOST};Database={PGDATABASE};Username={PGUSER};Password={PGPASSWORD}";
builder.Services.AddDbContext<DatabaseContext>(
    opt =>
    {
        opt.UseNpgsql(connectionString);
        if (builder.Environment.IsDevelopment())
        {
            opt
              .LogTo(Console.WriteLine, LogLevel.Information)
              .EnableSensitiveDataLogging()
              .EnableDetailedErrors();
        }
    }
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();


var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/test", () => "Hello World!");

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.MapHub<VoteHub>("/r/voteHub");
app.MapHub<PollHub>("/r/pollHub");



app.Run();

