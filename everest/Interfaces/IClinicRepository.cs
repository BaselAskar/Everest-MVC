using everest.DTOs;
using everest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IClinicRepository
    {
        Task AddClassificationToClinic(AppUser user, ClassificationDto classificationDto);

        Task AddClinic(AppUser user);

        Task RemoveClinic(AppUser user);
    }
}
