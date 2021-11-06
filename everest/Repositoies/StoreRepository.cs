using AutoMapper;
using everest.Data;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Repositoies
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext _data;
        public StoreRepository(DataContext data)
        {
            _data = data;
        }



        public async Task AddClassificationToStore(AppUser user,ClassificationDto classificationDto)
        {

            var classification = await _data.Classifications.SingleOrDefaultAsync(c => c.Title == classificationDto.Title 
                                    && c.Name == classificationDto.Name);

            var storeClassification = new StoreClassification
            {
                StoreId = user.Store.Id,
                Store = user.Store,
                ClassificationId = classification.Id,
                Classification = classification
            };

            _data.StoreClassifications.Add(storeClassification);
        }

        public async Task AddCompanyPhotoAsync(AppUser user, CompanyPhoto companyPhoto)
        {
            await _data.CompanyPhotos.AddAsync(companyPhoto);

            await _data.SaveChangesAsync();

            var store = await GetStoreAsync(user);

            store.CompanyPhotoId = companyPhoto.Id;
            store.CompanyPhoto = companyPhoto;

            

        }

        public async Task AddStore(AppUser user)
        {
            _data.Store.Add(new Store() { UserId = user.Id,User = user });

            await _data.SaveChangesAsync();

            var store = _data.Store.FirstOrDefault(s => s.UserId == user.Id);

            user.StoreId = store.Id;
            user.Store = store;

            await _data.SaveChangesAsync();
        }

        public async Task<CompanyPhoto> GetCompanyPhotoAsync(AppUser user)
        {
            var store = await GetStoreAsync(user);
            return await _data.CompanyPhotos.SingleOrDefaultAsync(cp => cp.Id == store.CompanyPhotoId);
        }

        public async Task<Store> GetStoreAsync(AppUser user)
        {
            return await _data.Store.SingleOrDefaultAsync(s => s.Id == user.StoreId);
        }

        public async Task RemoveCompanyPhotoAsync(int companyPhotoId)
        {
            var companyPhoto = await _data.CompanyPhotos.FindAsync(companyPhotoId);

            var store = await _data.Store.FindAsync(companyPhoto.StoreId);

            _data.CompanyPhotos.Remove(companyPhoto);

            await _data.SaveChangesAsync();

            store.CompanyPhotoId = 0;
            store.CompanyPhoto = null;

        }

        public async Task RemoveStore(AppUser user)
        {
            var store = await _data.Store.SingleOrDefaultAsync(s => s.Id == user.StoreId);

            user.StoreId = null;

            _data.Store.Remove(store);

        }

        public void UpdateCompanyPhoto(CompanyPhoto companyPhoto)
        {
            _data.Entry(companyPhoto).State = EntityState.Modified;
        }

        public void UpdateStore(Store store)
        {
            _data.Entry(store).State = EntityState.Modified;
        }
    }
}
