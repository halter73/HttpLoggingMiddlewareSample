using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096;
    options.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

app.UseHttpLogging();

// info: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware[4]
// ResponseBody: Hello World!
app.MapGet("/response-logged", () => "Hello world!");

// dbug: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware[6]
// Unrecognized Content-Type for body.
app.MapGet("/response not-logged", (HttpContext context) =>
{
    context.Response.ContentType = "foo";
    return "Hello World!";
});

app.Run();
