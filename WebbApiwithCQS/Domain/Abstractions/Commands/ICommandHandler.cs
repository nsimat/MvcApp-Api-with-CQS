using WebbApiwithCQS.Domain.Abstractions.Results;

namespace WebbApiwithCQS.Domain.Abstractions.Commands;

public interface ICommandHandler<TCommand> where TCommand : ICommandDefinition
{
    Task<Result<bool>> Execute(TCommand command);
}

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommandDefinition<TResult>
{
    Result<TResult> Execute(TCommand command);
}