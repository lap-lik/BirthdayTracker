namespace Infrastructure.Service.Options
{
    public class MediaOptions
    {
        public string BaseStoragePath { get; set; } = string.Empty;
        public int MaxImageSizeMb { get; set; }
    }
}