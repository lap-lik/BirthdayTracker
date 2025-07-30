using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Intefaces.Infrastructure.Service
{
    public interface IMediaService
    {
        Task<string?> SaveImageAsync(IFormFile imageFile, MediaType type);
        Task DeleteImageAsync(string? imageUrl);
        Task<string?> UpdateImageAsync(string? currentPhotoUrl, IFormFile? newImageFile, MediaType type);
    }
}