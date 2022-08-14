using CatalogService.Api.Middleware;
using CatalogService.Infrastructure;
using CategoryService.Application.Setup;
using IdentityServiceClient.Services;
using IdentityServiceClient.Storage;
using Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddEntityFrameworkSqlite();
builder.Services.AddLogging();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddCatalogDbContext();
builder.Services.AddSingleton<ITokenService>(s => new TokenService("SomeLongClientId"));
builder.Services.AddSingleton<IRoleDatabaseContext>(s => new RoleDatabaseContextL($"Filename=..\\IAMs.db;connection=shared"));
builder.Services.AddSingleton<IRoleService, RoleService>();
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.UseRouting();

app.Run();
