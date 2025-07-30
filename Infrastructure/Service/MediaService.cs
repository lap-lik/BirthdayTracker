using Core.Enums;
using Core.Intefaces.Infrastructure.Service;
using Infrastructure.Service.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Service
{
    public class MediaService : IMediaService
    {
        private readonly MediaOptions _mediaOptions;

        public MediaService(IOptions<MediaOptions> mediaOptions)
        {
            _mediaOptions = mediaOptions.Value;
        }

        public async Task<string?> SaveImageAsync(IFormFile imageFile, MediaType type)
        {
            if (!IsValidImage(imageFile))
                return null;

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            var folder = GetMediaFolder(type);
            var relativePath = Path.Combine(folder, $"{Guid.NewGuid()}{extension}");
            var fullPath = Path.Combine(_mediaOptions.BaseStoragePath, relativePath);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return NormalizePath(relativePath);
        }

        public Task DeleteImageAsync(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return Task.CompletedTask;

            var fullPath = Path.Combine(_mediaOptions.BaseStoragePath, relativePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }

        public async Task<string?> UpdateImageAsync(string? currentPhotoUrl, IFormFile? newImageFile, MediaType type)
        {
            var newPhotoUrl = await SaveImageAsync(newImageFile, type);

            if (newPhotoUrl != null)
            {
                await DeleteImageAsync(currentPhotoUrl);
            }
            return newPhotoUrl;
        }

        private static string GetMediaFolder(MediaType type) => type switch
        {
            MediaType.UserAvatar => "avatars",
            MediaType.PersonPhoto => "photos",
            _ => "general"
        };

        private bool IsValidImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return false;

            if (imageFile.Length > _mediaOptions.MaxImageSizeMb * 1024 * 1024)
                throw new ArgumentException($"Maximum file size: {_mediaOptions.MaxImageSizeMb}MB");

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            var validExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!validExtensions.Contains(extension))
                throw new ArgumentException("Acceptable formats: .jpg, .jpeg, .png");

            return true;
        }

        private static string NormalizePath(string path) =>
            path.Replace('\\', '/');
    }
}