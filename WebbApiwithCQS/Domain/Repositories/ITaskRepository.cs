using WebbApiwithCQS.Domain.Abstractions;
using WebbApiwithCQS.Domain.Commands;
using WebbApiwithCQS.Domain.Entities;
using WebbApiwithCQS.Domain.Queries;

namespace WebbApiwithCQS.Domain.Repositories;

public interface ITaskRepository :
    IQueryHandler<GetTasksQuery, IEnumerable<Tache>>,
    IQueryHandler<GetTaskByIdQuery, Tache>,
    ICommandHandler<InsertTaskCommand>,
    ICommandHandler<UpdateTaskCommand>,
    ICommandHandler<PatchTaskCommand>,
    ICommandHandler<DeleteTaskCommand>
{
}