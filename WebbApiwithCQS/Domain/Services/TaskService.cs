using Microsoft.EntityFrameworkCore;
using WebbApiwithCQS.Domain.Abstractions;
using WebbApiwithCQS.Domain.Commands;
using WebbApiwithCQS.Domain.Data;
using WebbApiwithCQS.Domain.Entities;
using WebbApiwithCQS.Domain.Queries;
using WebbApiwithCQS.Domain.Repositories;

namespace WebbApiwithCQS.Domain.Services;

public class TaskService(TaskDbContext taskDbContext) : ITaskRepository
{
    private readonly TaskDbContext _taskDbContext = taskDbContext ?? throw new ArgumentNullException(nameof(taskDbContext));

    public async Task<IEnumerable<Tache>> Execute(GetTasksQuery query)
    {
        var q = _taskDbContext.Taches.AsQueryable();

        return await q.ToListAsync();
    }

    public async Task<Tache> Execute(GetTaskByIdQuery query)
    {
        var tache = await _taskDbContext.Taches.FindAsync(query.Id);

        return tache;
    }

    public async Task<bool> Execute(InsertTaskCommand command)
    {
        if (command is null)
            throw new ArgumentNullException($"Command {command} must not be null!");

        var task = new Tache()
        {
            Titre = command.Titre
        };

        _taskDbContext.Taches.Add(task);
        int changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }

    public async Task<bool> Execute(UpdateTaskCommand command)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        var tacheToUpdate = await _taskDbContext.Taches.FindAsync(command.Id);

        if (tacheToUpdate is null)
            return false;

        if (command.Tache is null)
        {
            throw new ArgumentException("The field 'tache' of parameter 'command' must not be null.",
                nameof(command));
        }


        tacheToUpdate.Titre = command.Tache.Titre;
        tacheToUpdate.Cloturee = command.Tache.Cloturee;

        // Ne pas réaffecter DateCreation puisque la base gère la valeur par défaut
        var changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;

    }

    public async Task<bool> Execute(PatchTaskCommand command)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        var taskToPatch = await _taskDbContext.Taches.FindAsync(command.Id);

        if (taskToPatch is null)
            return false;

        if (command.Task is null)
            throw new ArgumentNullException(nameof(command));

        taskToPatch.Cloturee = command.Task.Cloturee;

        int changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }

    public async Task<bool> Execute(DeleteTaskCommand command)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        var tache = await _taskDbContext.Taches.FindAsync(command.Id);

        if (tache is null)
            return false;

        _taskDbContext.Remove(tache);
        var changes = await _taskDbContext.SaveChangesAsync();

        return changes > 0;
    }
}