namespace CategoryService.Application.Interfaces.Queries;

public interface IQueryDispatcher
{
    Task<TResult> Send<T, TResult>(T query);
}