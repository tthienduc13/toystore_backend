using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Services.Configurations;
using Services.Interfaces;

namespace Services.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySetting> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> UploadAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result;
        }

        return new ImageUploadResult
        {
            Error = new Error { Message = "File is empty." }
        };
    }
}