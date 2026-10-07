using WebbApiwithCQS.Domain.Abstractions.Results;

namespace WebbApiwithCQS.Domain.Abstractions.Queries
{
    public interface IQueryAsyncHandler<TQuery, TResult> where TQuery : IQueryDefinition<TResult>
    {
        Task<Result<TResult>> ExecuteAsync(TQuery query);
    }
}
