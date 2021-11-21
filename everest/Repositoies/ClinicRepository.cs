using everest.Data;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Repositoies
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly DataContext _data;
        public ClinicRepository(DataContext data)
        {
            _data = data;
        }


        public async Task AddClinicAsync(Clinic clinic)
        {
            await _data.Clinics.AddAsync(clinic);

        }

        public async Task<Clinic> GetClinicAsync(AppUser user)
        {
            return await _data.Clinics.SingleOrDefaultAsync(cl => cl.UserId == user.Id);
        }

        public void RemoveClinic(Clinic clinic)
        {
            _data.Clinics.Remove(clinic);
        }
    }
}
