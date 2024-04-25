namespace Demo.Api.Models;

public class Todo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;
}
