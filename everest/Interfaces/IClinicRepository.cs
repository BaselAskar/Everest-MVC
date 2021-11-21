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

        Task AddClinicAsync(Clinic clinic);
        Task<Clinic> GetClinicAsync(AppUser user);
        void RemoveClinic(Clinic clinic);
    }
}
