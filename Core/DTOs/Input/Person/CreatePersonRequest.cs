using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Input.Person
{
    public record CreatePersonRequest(
        [Required] string Name,
        [Required] DateOnly Birthday,
        PersonType? Type,
        IFormFile? Photo);
}