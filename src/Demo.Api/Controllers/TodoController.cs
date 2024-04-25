using Demo.Api.Commands.CreateTodo;
using Demo.Api.Dtos;
using Demo.Api.Queries.GetTodos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Wemogy.CQRS.Commands.Abstractions;
using Wemogy.CQRS.Queries.Abstractions;

namespace Demo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController
{
    private readonly ICommands _commands;
    private readonly IQueries _queries;

    public TodoController(ICommands commands, IQueries queries)
    {
        _commands = commands;
        _queries = queries;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoDto>>> ListTodos()
    {
        var query = new GetTodosQuery();
        var result = await _queries.QueryAsync(query);
        var dto = result.Adapt<List<TodoDto>>();
        return new OkObjectResult(dto);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoRequest request)
    {
        // Map the request (DTO) to the command (Model)
        var command = request.Adapt<CreateTodoCommand>();

        // Run the command
        var result = await _commands.RunAsync(command);

        // Map the result (Model) to the response (DTO)
        var dto = result.Adapt<TodoDto>();
        return new OkObjectResult(dto)
        {
            StatusCode = 201
        };
    }
}
