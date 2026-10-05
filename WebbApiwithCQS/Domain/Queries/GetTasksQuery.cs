using WebbApiwithCQS.Domain.Abstractions;
using WebbApiwithCQS.Domain.Entities;

namespace WebbApiwithCQS.Domain.Queries;

public record GetTasksQuery() : IQueryDefinition<IEnumerable<Tache>>;