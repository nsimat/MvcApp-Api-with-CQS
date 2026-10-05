using WebbApiwithCQS.Domain.Abstractions;
using WebbApiwithCQS.Domain.Entities;

namespace WebbApiwithCQS.Domain.Commands;

public record PatchTaskCommand(int Id, Tache Task) : ICommandDefinition;