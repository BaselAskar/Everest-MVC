using AutoMapper;
using everest.Data;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Repositoies
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext _data;
        private readonly IMapper _mapper;
        public StoreRepository(DataContext data,IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }




        public async Task AddStorePhotoAsync(Store store, StorePhoto storePhoto)
        {
            var storePhotoEntity = await _data.StorePhotos.AddAsync(storePhoto);

            await _data.SaveChangesAsync();

            store.StorePhotoId = storePhotoEntity.Entity.Id;
            store.StorePhoto = storePhotoEntity.Entity;

        }


        public async Task AddStoreAsync(Store store)
        {
            await _data.Stores.AddAsync(store);
        }

        public async Task<List<ClassificationDto>> GetClassificationStoreAsync(Store store)
        {
            return await _data.ClassificationStores
                .Where(cs => cs.StoreId == store.Id)
                .Select(cs => _mapper.Map<ClassificationDto>(cs.Classification))
                .ToListAsync();
        }

        public async Task<StorePhoto> GetStorePhotoAsync(Store store)
        {
            return await _data.StorePhotos.FindAsync(store.StorePhotoId);
        }

        public async Task<Store> GetStoreAsync(AppUser user)
        {
            return await _data.Stores
                .Include(s => s.Products)
                .SingleOrDefaultAsync(s => s.UserId == user.Id);
            
        }

        public async Task RemoveStorePhotoAsync(int storePhotoId)
        {
            var store = await _data.Stores.SingleOrDefaultAsync(s => s.StorePhotoId == storePhotoId);
            var storePhoto = await _data.StorePhotos.FindAsync(storePhotoId);
            _data.StorePhotos.Remove(storePhoto);
            store.StorePhotoId = 0;
            store.StorePhoto = null;
        }

        public void RemoveStore(Store store)
        {

            _data.Stores.Remove(store);
        }

        public void UpdateStorePhoto(StorePhoto storePhoto)
        {
            _data.Entry(storePhoto).State = EntityState.Modified;
        }

        public void UpdateStore(Store store)
        {
            _data.Entry(store).State = EntityState.Modified;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _data.Products
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<List<ProductDto>> GetProductsAsync(Store store)
        {
            return await _data.Products
                .Where(p => p.StoreId == store.Id)
                .Include(p => p.Photos)
                .Select(p => _mapper.Map<ProductDto>(p))
                .ToListAsync();
        }


        public void UpdateProduct(Product product)
        {
            _data.Entry(product).State = EntityState.Modified;
        }

        public void RemoveProduct(Product product)
        {
            _data.Products.Remove(product);
        }

    }
}
