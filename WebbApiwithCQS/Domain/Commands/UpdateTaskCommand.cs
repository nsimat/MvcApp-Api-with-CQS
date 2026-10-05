using WebbApiwithCQS.Domain.Abstractions;
using WebbApiwithCQS.Domain.Entities;

namespace WebbApiwithCQS.Domain.Commands;

public record UpdateTaskCommand(int Id, Tache Tache) : ICommandDefinition;