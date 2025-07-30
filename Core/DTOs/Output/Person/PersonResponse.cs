using Core.Enums;

namespace Core.DTOs.Output.Person
{
    public record PersonResponse(
        Guid Id,
        string Name,
        int Age,
        DateOnly Birthday,
        PersonType Type,
        string? PhotoUrl);
}