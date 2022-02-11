using everest.DTOs;
using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IStoreClassificationRepository
    {
        Task<IEnumerable<ClassificationDto>> GetStoreClassificatiosAsyc();
        Task<StoreClassification> GetStoreClassificationByTitleAndNameAsync(ClassificationDto classificationDto);
        void AddStoreClassification(ClassificationDto classificationDto);
    }
}
