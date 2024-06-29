using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskApi.Entity;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskApi.DTOs;

public record UserDto(Guid Id, string Name);
public record TaskDto(Guid UserId, DateTime Date, DateTime StartTime, DateTime EndTime, string Subject,string Description);
public record TaskUpdateDto(Guid Id, Guid UserId, DateTime Date, DateTime StartTime, DateTime EndTime, string Subject, string Description);

