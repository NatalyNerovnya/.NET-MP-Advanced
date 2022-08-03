using IdentityServer.Services;
using IdentityServer.Services.Interfaces;
using IdentityServer.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserDatabaseContext>(s => new UserDatabaseContext($"Filename=IAMs.db;connection=shared"));
builder.Services.AddScoped<IRoleDatabaseContext>(s => new RoleDatabaseContextL($"Filename=IAMs.db;connection=shared"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<ITokenService>(s => new TokenService("SomeClientId"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
