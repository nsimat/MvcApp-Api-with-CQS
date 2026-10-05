using WebbApiwithCQS.Domain.Abstractions;

namespace WebbApiwithCQS.Domain.Commands;

public record InsertTaskCommand(string Titre) : ICommandDefinition;