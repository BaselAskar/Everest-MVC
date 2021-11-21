﻿using everest.DTOs;
using everest.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<ClassificationDto>> GetClassificationStoreAsync(Store store);
        Task AddStoreAsync(Store store);
        Task AddStorePhotoAsync(Store store, StorePhoto storePhoto);
        Task RemoveStorePhotoAsync(int StorePhotoId);
        Task<StorePhoto> GetStorePhotoAsync(Store store);
        void UpdateStorePhoto(StorePhoto companyPhoto);
        Task<Store> GetStoreAsync(AppUser user);
        Task<Product> GetProductByPublicIdAsync(string publicId);
        void RemoveStore(Store store);
        void UpdateStore(Store store);
    }
}
