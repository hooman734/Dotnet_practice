using WAPP.API.Models;

namespace WAPP.API.Data;

public interface ICommandRepo
{
    Task SaveChangesTask();
    Task<Command?> GetCommandByIdTask(Guid id);
    Task<IEnumerable<Command>> GetAllCommandsTask();
    Task CreateCommandTask(Command cmd);

    void DeleteCommand(Command cmd);
}