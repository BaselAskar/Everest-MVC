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
    public class StoreClassificatioRepository : IStoreClassificationRepository
    {

        private readonly DataContext _data;
        private readonly IMapper _mapper;

        public StoreClassificatioRepository(DataContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        public void AddStoreClassification(ClassificationDto classificationDto)
        {
            var storeClassification = _mapper.Map<StoreClassification>(classificationDto);
            _data.StoreClassifications.Add(storeClassification);
        }

        public async Task<StoreClassification> GetStoreClassificationByTitleAndNameAsync(ClassificationDto classificationDto)
        {
            return await _data.StoreClassifications
                .SingleOrDefaultAsync(sc => sc.Title == classificationDto.Title && sc.Name == classificationDto.Name);
        }

        public async Task<IEnumerable<ClassificationDto>> GetStoreClassificatiosAsyc()
        {
            return await _data.StoreClassifications
                    .Select(c => new ClassificationDto { Title = c.Title, Name = c.Name })
                    .ToListAsync();
        }
    }
}
