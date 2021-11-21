using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using everest.Helpers;
using everest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Services
{
    public class PhotoServices : IPhotoServices
    {
        private readonly Cloudinary _cloudinary;
        public PhotoServices(IOptions<CloudinarySettings> config)
        {
            var account = new Account
                (
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddStorePhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var imageParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(706).Height(160)
                };

                uploadResult = await _cloudinary.UploadAsync(imageParams);
            }

            return uploadResult;
        }


        public async Task<ImageUploadResult> AddProductPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var imageParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                                        .Height(150).Width(150).Quality("auto")
                };

                uploadResult = await _cloudinary.UploadAsync(imageParams);
            }

            return uploadResult;

        }

        public async Task<DeletionResult> RemovePhoto(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);

            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult;
        }
    }
}
