using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Input.User
{
    public record ChangePasswordRequest(
    [Required] string CurrentPassword,
    [Required] string NewPassword);
}
