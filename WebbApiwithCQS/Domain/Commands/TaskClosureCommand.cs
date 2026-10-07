using WebbApiwithCQS.Domain.Abstractions.Commands;
using WebbApiwithCQS.Domain.Entities;

namespace WebbApiwithCQS.Domain.Commands;

public record TaskClosureCommand(int Id) : ICommandDefinition<bool>;