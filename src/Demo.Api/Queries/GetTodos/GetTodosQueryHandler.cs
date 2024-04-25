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

    public Task<List<Todo>> HandleAsync(GetTodosQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(_todoRepository.ToList());
    }
}
