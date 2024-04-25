using Demo.Api.Models;
using Wemogy.CQRS.Commands.Abstractions;

namespace Demo.Api.Commands.CreateTodo;

public class CreateTodoCommand : ICommand<Todo>
{
    public string Name { get; }

    public CreateTodoCommand(string name)
    {
        Name = name;
    }
}
