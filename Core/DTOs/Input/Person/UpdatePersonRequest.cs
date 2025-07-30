using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Input.Person
{
    public record UpdatePersonRequest( 
        string? Name,
        DateOnly? Birthday,
        PersonType? Type, 
        IFormFile? Photo);
}