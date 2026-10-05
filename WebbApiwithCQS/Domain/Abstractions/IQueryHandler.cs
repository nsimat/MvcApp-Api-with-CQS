namespace WebbApiwithCQS.Domain.Abstractions;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQueryDefinition<TResult>
{
    Task<TResult> Execute(TQuery query);
}