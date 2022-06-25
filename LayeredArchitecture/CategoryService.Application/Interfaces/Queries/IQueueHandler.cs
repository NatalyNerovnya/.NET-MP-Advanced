namespace CategoryService.Application.Interfaces.Queries;

public interface IQueryHandler<in T, TResult>
{
    Task<TResult> Handle(T query);
}
