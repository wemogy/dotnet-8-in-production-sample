using Demo.Api.Models;
using Wemogy.CQRS.Queries.Abstractions;

namespace Demo.Api.Queries.GetTodos;

public class GetTodosQuery : IQuery<List<Todo>>
{
}
