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



        public async Task<Classification> GetClassificationByTitleAndName(string title, string name)
        {
            return await _data.Classifications
                            .SingleOrDefaultAsync(c => c.Name == name && c.Title == title);

        }

        public async Task<IEnumerable<ClassificationDto>> GetClassifications()
        {
            return await _data.Classifications
                .Select(c => _mapper.Map<ClassificationDto>(c))
                .ToListAsync();
        }

    }
}
