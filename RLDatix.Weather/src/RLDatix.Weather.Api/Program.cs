using Microsoft.AspNetCore.HttpOverrides;
using RLDatix.Weather.Application;
using RLDatix.Weather.Infrastructure;
using RLDatix.Weather.Infrastructure.Configuration;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastruture();
builder.Services.Configure<OpenWeatherMapHttpConfiguration>(builder.Configuration.GetSection(nameof(OpenWeatherMapHttpConfiguration)));



// Configure a global rate limiter
builder.Services.AddRateLimiter(options =>
{
    // Configure a fixed window rate limiter that applies globally
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: "global", // Use a constant key to apply globally
            factory: partition =>
                new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10, // Number of requests allowed
                    Window = TimeSpan.FromSeconds(60), // Time window
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 2, // Queue up to 2 requests if the limit is hit
                }));

    // Handle the rate limit exceeded event
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.");
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable the rate limiter middleware
app.UseRateLimiter(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
