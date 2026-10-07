using WebbApiwithCQS.Domain.Abstractions.Results;

namespace WebbApiwithCQS.Domain.Abstractions.Commands
{
    public interface ICommandAsyncHandler<TCommand> where TCommand : ICommandDefinition
    {
        Task<Result> ExecuteAsync(TCommand command);
    }

    public interface ICommandAsyncHandler<TCommand, TResult> where TCommand : ICommandDefinition<TResult>
    {
        Task<Result<TResult>> ExecuteAsync(TCommand command);
    }
}
