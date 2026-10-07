using Microsoft.EntityFrameworkCore;
using WebbApiwithCQS.Domain.Abstractions.Results;
using WebbApiwithCQS.Domain.Commands;
using WebbApiwithCQS.Domain.Data;
using WebbApiwithCQS.Domain.Entities;
using WebbApiwithCQS.Domain.Queries;
using WebbApiwithCQS.Domain.Repositories;

namespace WebbApiwithCQS.Domain.Services;

public class TaskService(TaskDbContext taskDbContext) : ITaskRepository
{
    private readonly TaskDbContext _taskDbContext = taskDbContext ?? throw new ArgumentNullException(nameof(taskDbContext));

    public Task<Result<IEnumerable<Tache>>> ExecuteAsync(GetTasksQuery query)
    {
        var tasks = _taskDbContext.Taches.AsNoTracking().ToList();

        return Task.FromResult((Result<IEnumerable<Tache>>)tasks);
    }

    public async Task<Result<Tache>> ExecuteAsync(GetTaskByIdQuery query)
    {
        var tache = await _taskDbContext.Taches.FindAsync(query.Id);

        return (Result<Tache>?)tache ?? TaskErrors.NotFound;
    }

    public async Task<Result<bool>> ExecuteAsync(InsertTaskCommand command)
    {
        if (command is null)
            throw new ArgumentNullException($"Command {command} must not be null!");

        var task = new Tache()
        {
            Titre = command.Titre
        };

        _taskDbContext.Taches.Add(task);
        var changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }

    public async Task<Result<bool>> ExecuteAsync(UpdateTaskCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (command.Tache is null)
            throw new ArgumentException("The field 'Tache' of parameter 'command' must not be null.",
                 nameof(command));

        var tacheToUpdate = await _taskDbContext.Taches.FindAsync(command.Id);

        if (tacheToUpdate is null)
            return false;

        tacheToUpdate.Titre = command.Tache.Titre;
        tacheToUpdate.DateCreation = command.Tache.DateCreation;

        // Ne pas réaffecter DateCreation puisque la base gère la valeur par défaut
        var changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }

    public async Task<Result<bool>> ExecuteAsync(TaskClosureCommand closureCommand)
    {
        ArgumentNullException.ThrowIfNull(closureCommand);

        var taskToClose = await _taskDbContext.Taches.FindAsync(closureCommand.Id);

        if (taskToClose is null)
            return false;

        taskToClose.Cloturee = true;

        var changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }

    public async Task<Result<bool>> ExecuteAsync(DeleteTaskCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var tache = await _taskDbContext.Taches.FindAsync(command.Id);

        if (tache is null)
            return false;

        _taskDbContext.Remove(tache);
        var changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }
}