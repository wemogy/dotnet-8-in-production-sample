namespace Demo.Api.Dtos;

public class CreateTodoRequest
{
    public string Name { get; set; }

    public CreateTodoRequest()
    {
        Name = string.Empty;
    }
}
