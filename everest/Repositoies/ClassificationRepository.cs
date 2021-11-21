using AutoMapper;
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
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly DataContext _data;
        private readonly IMapper _mapper;

        public ClassificationRepository(DataContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }



        public async Task AddClassification(ClassificationDto classificationDto)
        {
            var classification = _mapper.Map<Classification>(classificationDto);
            await _data.Classifications.AddAsync(classification);
        }

        public async Task AddClassificationToClinicAsync(Clinic clinic, ClassificationDto classificationDto)
        {
            var classification = await _data.Classifications
                .SingleOrDefaultAsync(c => c.Name == classificationDto.Name && c.Title == classificationDto.Title);

            var classificationClinic = new ClassificationClinic
            {
                ClassificationId = classification.Id,
                Classification = classification,
                ClinicId = clinic.Id,

            };

            await _data.ClassificationClinics.AddAsync(classificationClinic);
        }

        public async Task AddClassificationToStoreAsync(Store store, ClassificationDto classificationDto)
        {
            var classification = await _data.Classifications
                .SingleOrDefaultAsync(c => c.Name == classificationDto.Name && c.Title == classificationDto.Title);

            var classificationStore = new ClassificationStore
            {
                ClassificationId = classification.Id,
                Classification = classification,
                StoreId = store.Id,
                Store = store
            };

            await _data.ClassificationStores.AddAsync(classificationStore);

        }

        public async Task<IEnumerable<ClassificationDto>> GetClassifications()
        {
            return await _data.Classifications
                .Select(c => _mapper.Map<ClassificationDto>(c))
                .ToListAsync();
        }

    }
}
