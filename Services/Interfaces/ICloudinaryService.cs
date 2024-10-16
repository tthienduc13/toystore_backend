using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadAsync(IFormFile file);
}
