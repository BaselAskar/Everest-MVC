using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IPhotoServices
    {
        Task<ImageUploadResult> AddStorePhotoAsync(IFormFile file);
        Task<ImageUploadResult> AddProductPhotoAsync(IFormFile file);
        Task<ImageUploadResult> AddSlidePhotoAsync(IFormFile file);
        Task<DeletionResult> RemovePhoto(string publicId);
    }
}
