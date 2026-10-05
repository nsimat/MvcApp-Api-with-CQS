namespace WebbApiwithCQS.Domain.Abstractions;

public interface ICommandHandler<TCommand> where TCommand : ICommandDefinition
{
    Task<bool> Execute(TCommand command);
}