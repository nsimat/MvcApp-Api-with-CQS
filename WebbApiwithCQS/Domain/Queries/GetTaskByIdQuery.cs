using WebbApiwithCQS.Domain.Abstractions;
using WebbApiwithCQS.Domain.Entities;

namespace WebbApiwithCQS.Domain.Queries;

public record GetTaskByIdQuery(int Id) : IQueryDefinition<Tache>;