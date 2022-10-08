using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WAPP.API.Data;
using WAPP.API.Dtos;
using WAPP.API.Models;


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

// Register Database for EF
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnectionBuilder.ConnectionString));

// Register Repository
builder.Services.AddScoped<ICommandRepo, CommandRepo>();

// Configure Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Controllers

// Get By ID
app.MapGet("/api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, Guid id) =>
{
    var command = await repo.GetCommandByIdTask(id);
    if (command != null)
    {
        return Results.Ok(mapper.Map<CommandReadDto>(command));
    }
    return Results.NotFound();
});

// Get All
app.MapGet("/api/v1/commands", async (ICommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetAllCommandsTask();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

// Create
app.MapPost("/api/v1/commands", async (ICommandRepo repo, IMapper mapper, CommandCreateDto commandCreateDto) =>
{
    var command = mapper.Map<Command>(commandCreateDto);
    await repo.CreateCommandTask(command);
    await repo.SaveChangesTask();

    var commandRead = mapper.Map<CommandReadDto>(command);
    return Results.Created($@"api/v1/commands/{commandRead.Id}", commandRead);
});

// Update
app.MapPut("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, Guid id, CommandUpdateDto cmdUpdateDto) =>
{
    var command = await repo.GetCommandByIdTask(id);
    if (command == null)
    {
        return Results.NotFound();
    }
    mapper.Map(cmdUpdateDto, command);
    await repo.SaveChangesTask();
    return Results.NoContent();
});
// Delete
app.MapDelete("/api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, Guid id) =>
{
    var command = await repo.GetCommandByIdTask(id);
    if (command == null)
    {
        return Results.NotFound();
    }
    repo.DeleteCommand(command);
    await repo.SaveChangesTask();
    return Results.NoContent();
});


app.MapGet("/", () => "Not Found Page!");

app.Run();