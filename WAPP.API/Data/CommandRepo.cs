using System.Xml;
using Microsoft.EntityFrameworkCore;
using WAPP.API.Migrations;
using WAPP.API.Models;

namespace WAPP.API.Data;

public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _ctx;

    public CommandRepo(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task SaveChangesTask()
    {
        await _ctx.SaveChangesAsync();
    }

    public async Task<Command?> GetCommandByIdTask(Guid id)
    {
        return await _ctx.Commands!.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Command>> GetAllCommandsTask()
    {
        return await _ctx.Commands!.ToListAsync();
    }

    public async Task CreateCommandTask(Command cmd)
    {
        if (cmd == null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }
        await _ctx.AddAsync(cmd);
    }

    public void DeleteCommand(Command cmd)
    {
        if (cmd == null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }
        _ctx.Commands.Remove(cmd);
    }
}