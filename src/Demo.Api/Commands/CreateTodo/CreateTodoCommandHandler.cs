using Demo.Api.Models;
using Demo.Api.Repositories;
using Wemogy.CQRS.Commands.Abstractions;

namespace Demo.Api.Commands.CreateTodo;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Todo>
{
    private readonly TodoRepository _todoRepository;

    public CreateTodoCommandHandler(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public Task<Todo> HandleAsync(CreateTodoCommand command)
    {
        var todo = new Todo()
        {
            Name = command.Name
        };

        // Save to the repository
        _todoRepository.Add(todo);

        // Return the created entity
        return Task.FromResult(todo);
    }
}
