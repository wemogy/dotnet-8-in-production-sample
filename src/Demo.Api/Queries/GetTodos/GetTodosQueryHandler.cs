using Demo.Api.Models;
using Demo.Api.Repositories;
using Wemogy.CQRS.Queries.Abstractions;

namespace Demo.Api.Queries.GetTodos;

public class GetTodosQueryHandler : IQueryHandler<GetTodosQuery, List<Todo>>
{
    private readonly TodoRepository _todoRepository;

    public GetTodosQueryHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<List<Todo>> HandleAsync(GetTodosQuery query, CancellationToken cancellationToken)
    {
        using var activity = Observability.Default.StartActivity("Getting tasks from database");
        await Task.Delay(500);
        return _todoRepository.ToList();
    }
}
