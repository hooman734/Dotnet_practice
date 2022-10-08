namespace WAPP.API.Dtos;

public class CommandReadDto
{
    public Guid Id { get; init; }

    public string? HowTo { get; set; }

    public string? Platform { get; set; }

    public string? CommandLine { get; set;}
}