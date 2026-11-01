using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Api.Extensions;
using UserManagement.Api.Middleware;
using UserManagement.Api.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


// ------------------------------------
// Controllers
// ------------------------------------

builder.Services.AddControllers();

builder.Services.AddCustomValidation();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// ------------------------------------
// Application Services
// ------------------------------------

builder.Services.AddApplicationServices(
    builder.Configuration);


builder.Services.AddJwtAuthentication(
    builder.Configuration);


// ------------------------------------
// Swagger
// ------------------------------------

builder.Services.AddCustomSwagger();


// ------------------------------------
// Build
// ------------------------------------

var app = builder.Build();


// Database Seeder
await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await DbSeeder.SeedAsync(dbContext);
}


// ------------------------------------
// Swagger
// ------------------------------------

app.UseCustomSwagger();


// ------------------------------------
// Middleware
// ------------------------------------

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();


// ------------------------------------
// Authentication
// ------------------------------------

app.UseAuthentication();

app.UseAuthorization();


// ------------------------------------
// Controllers
// ------------------------------------

app.MapControllers();


// ------------------------------------
// Run
// ------------------------------------

app.Run();