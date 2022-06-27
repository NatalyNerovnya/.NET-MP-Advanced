using CategoryService.Application.Interfaces.Queries;

namespace CategoryService.Application.Queries;

public class QueryDispatcher: IQueryDispatcher
{
    private readonly IServiceProvider _service;

    public QueryDispatcher(IServiceProvider service)
    {
        _service = service;
    }

    public Task<TResult> Send<T, TResult>(T query)
    {
        var handler = _service.GetService(typeof(IQueryHandler<T, TResult>));
        if (handler != null)
        {
            return ((IQueryHandler<T, TResult>)handler).Handle(query);

        }
        else
        {
            throw new Exception($"Query doesn't have any handler {query?.GetType().Name}");

        }
    }
}