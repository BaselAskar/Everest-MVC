using everest.DTOs;
using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IStoreRepository
    {
        Task AddClassificationToStore(AppUser user,ClassificationDto classificationDto);
        Task AddStore(AppUser user);
        Task AddCompanyPhotoAsync(AppUser user, CompanyPhoto companyPhoto);
        Task RemoveCompanyPhotoAsync(int companyPhotoId);
        Task<CompanyPhoto> GetCompanyPhotoAsync(AppUser user);
        void UpdateCompanyPhoto(CompanyPhoto companyPhoto);
        Task<Store> GetStoreAsync(AppUser user);
        Task RemoveStore(AppUser user);
        void UpdateStore(Store store);
    }
}
