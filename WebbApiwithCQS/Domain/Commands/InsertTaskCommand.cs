using WebbApiwithCQS.Domain.Abstractions.Commands;

namespace WebbApiwithCQS.Domain.Commands;

public record InsertTaskCommand(string Titre) : ICommandDefinition<bool>;