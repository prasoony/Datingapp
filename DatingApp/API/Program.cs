using System;
using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);


builder.Services.AddCors();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
 using var  scope =app.Services.CreateScope();
 var services=scope.ServiceProvider;
 try{
    var context=services.GetRequiredService<Datacontext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
 }
 catch(Exception ex)
 {
    var Logger =services.GetService<ILogger<Program>>();
    Logger.LogError(ex, " An error occurred during migration");
 }
app.Run();
