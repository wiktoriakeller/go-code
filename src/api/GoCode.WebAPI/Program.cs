using GoCode.Application.Common.Extensions;
using GoCode.Infrastructure.Extensions;
using GoCode.WebAPI.Extensions;
using GoCode.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();
builder.Services.ConfigureCors();

var app = builder.Build();

app.ApplyMigrations();

await app.SeedDatabase();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseIdentityMiddleware();

app.UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();
