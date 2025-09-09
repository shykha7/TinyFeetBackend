namespace TinyFeetBackend.CloudinaryS
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
