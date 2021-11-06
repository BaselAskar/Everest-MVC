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

        public async Task AddClassificationToClinic(AppUser user, ClassificationDto classificationDto)
        {
            var classification = await _data.Classifications.SingleOrDefaultAsync(c => c.Title == classificationDto.Title
                        && c.Name == classificationDto.Name);

            var clinicClassification = new ClinicClassification
            {
                ClinicId = user.Clinic.Id,
                ClassificationId = classification.Id
            };

            _data.ClinicClassifications.Add(clinicClassification);

        }

        public async Task AddClinic(AppUser user)
        {
            _data.Clinic.Add(new Clinic() { UserId = user.Id });

            await _data.SaveChangesAsync();

            var clinic = _data.Clinic.FirstOrDefault(s => s.UserId == user.Id);

            user.ClinicId = clinic.Id;

            await _data.SaveChangesAsync();

        }

        public async Task RemoveClinic(AppUser user)
        {
            var clinic = await _data.Clinic.SingleOrDefaultAsync(c => c.Id == user.ClinicId);

            user.ClinicId = null;

            _data.Clinic.Remove(clinic);
        }
    }
}
