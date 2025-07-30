using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Input.Auth
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}