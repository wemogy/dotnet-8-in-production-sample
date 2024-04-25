namespace Demo.Api.Models;

public class Todo
{
    public string Id { get; set; }

    public string Name { get; set; }

    public Todo()
    {
        Id = Guid.NewGuid().ToString();
        Name = string.Empty;
    }
}
