using WebbApiwithCQS.Domain.Abstractions.Commands;
using WebbApiwithCQS.Domain.Abstractions.Queries;
using WebbApiwithCQS.Domain.Commands;
using WebbApiwithCQS.Domain.Entities;
using WebbApiwithCQS.Domain.Queries;

namespace WebbApiwithCQS.Domain.Repositories;

public interface ITaskRepository :
    IQueryAsyncHandler<GetTasksQuery, IEnumerable<Tache>>,
    IQueryAsyncHandler<GetTaskByIdQuery, Tache>,
    ICommandAsyncHandler<InsertTaskCommand, bool>,
    ICommandAsyncHandler<UpdateTaskCommand, bool>,
    ICommandAsyncHandler<TaskClosureCommand, bool>,
    ICommandAsyncHandler<DeleteTaskCommand, bool>
{
}