using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskApi.Entity;

public record TaskU
{
    //public TaskU(User user)
    //{
    //    this.User = user;
    //}

    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; init; }

    [ForeignKey("UserId")]
    public required User User { get; init; }

    [Required]
    public DateTime Date { get; init; }
    [Required]
    public DateTime StartTime { get; init; }
    [Required]
    public DateTime EndTime { get; init; }
    [Required]
    public string Subject { get; init; } = string.Empty;
    [Required]
    public string Description { get; init; } = string.Empty;

}

