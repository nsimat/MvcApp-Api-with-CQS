using WebbApiwithCQS.Domain.Abstractions.Results;

namespace WebbApiwithCQS.Domain.Abstractions.Queries;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQueryDefinition<TResult>
{
    Result<TResult> Execute(TQuery query);
}