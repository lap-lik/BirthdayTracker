using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Input.User
{
    public record UpdateUserRequest(
    string? UserName,
    string? Email,
    IFormFile? Avatar,
    string? Password);
}
