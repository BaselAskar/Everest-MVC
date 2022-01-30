using everest.DTOs;
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
        Task<List<Store>> GetStoresAsync();
        Task<AppUser> GetUserFromStoreAsync(int storeId);
        Task AddStoreAsync(Store store);
        Task AddStorePhotoAsync(Store store, StorePhoto storePhoto);
        Task RemoveStorePhotoAsync(int StorePhotoId);
        Task<StorePhoto> GetStorePhotoAsync(Store store);
        void UpdateStorePhoto(StorePhoto companyPhoto);
        Task<Store> GetStoreAsync(AppUser user);
        Task<Store> GetStoreWithStoerPhotoAsync(AppUser user);
        Task<Product> GetProductByIdAsync(string id);
        void UpdateProduct(Product product);
        Task<List<ProductDto>> GetProductsAsync(Store store);
        void RemoveProduct(Product product);
        void RemoveStore(Store store);
        void UpdateStore(Store store);
    }
}
