using CryptoLocalBack.Domain;
using CryptoLocalBack.Extensions;
using CryptoLocalBack.Infrastructure;
using System.Text;
using Quartz;
using CryptoLocalBack.Model;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

string? dbConnString = builder.Configuration
    .GetConnectionString("DbConnString")
    ?? "Server=localhost;Port=5432;Database=cryptodb;User Id=postgres;Password=postgres;";
builder.Services.AddNpgsqlDbContext<CryptoLocalBackDbContext>(dbConnString);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSingleton<IConfiguration>(configuration); ;
builder.Services.AddSingleton(new SystemctlExtension(configuration));

builder.Services.AddCors();
var app = builder
    .Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseCors(x => x
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)); // allow credentials

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.RunDatabaseMigrations();
app.Run();
