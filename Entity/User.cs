using System.ComponentModel.DataAnnotations;
namespace TaskApi.Entity;

public record User
{
    [Key]
    public Guid Id { get; init; }// Guid

    [Required]
    public string Name { get; init; } = string.Empty;
}
