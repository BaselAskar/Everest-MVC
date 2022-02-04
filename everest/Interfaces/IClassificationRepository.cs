using everest.DTOs;
using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IClassificationRepository
    {
        Task<IEnumerable<ClassificationDto>> GetClassifications();
        Task AddClassification(ClassificationDto classification);
        Task<Classification> GetClassificationByTitleAndName(string title, string name);

        
    }
}
