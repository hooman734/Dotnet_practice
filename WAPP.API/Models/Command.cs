using System.ComponentModel.DataAnnotations;

namespace WAPP.API.Models;

public record Command
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public string? HowTo { get; set; }

    [Required, MaxLength(5)]
    public string? Platform { get; set; }

    [Required]
    public string? CommandLine { get; set;}
}