using System.ComponentModel.DataAnnotations;

namespace WAPP.API.Dtos;

public record CommandUpdateDto
{
    [Required]
    public string? HowTo { get; set; }

    [Required, MaxLength(5)]
    public string? Platform { get; set; }

    [Required]
    public string? CommandLine { get; set;}
}