using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .Build();
    });
});

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();
//});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToController("Index", "Fallback");
});

app.MapControllers();

app.Run();