using WebbApiwithCQS.Domain.Abstractions;

namespace WebbApiwithCQS.Domain.Commands;

public record DeleteTaskCommand(int Id) : ICommandDefinition;