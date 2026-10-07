using WebbApiwithCQS.Domain.Abstractions.Commands;

namespace WebbApiwithCQS.Domain.Commands;

public record DeleteTaskCommand(int Id) : ICommandDefinition<bool>;