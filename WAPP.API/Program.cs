using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WAPP.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// New routes
var sqlConnectionBuilder = new SqlConnectionStringBuilder()
{
    ConnectionString = builder.Configuration.GetConnectionString("Default"),
    UserID = builder.Configuration.GetSection("UserId").Value,
    Password = builder.Configuration.GetSection("Password").Value,
};

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnectionBuilder.ConnectionString));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Not Found Page!");

app.Run();