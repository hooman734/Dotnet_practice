using Microsoft.EntityFrameworkCore;
using WAPP.API.Models;

namespace WAPP.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Command> Commands => Set<Command>();
}