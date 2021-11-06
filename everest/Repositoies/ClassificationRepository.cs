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

        public ClassificationRepository(DataContext data, IMapper mapper)
        {
            _data = data;
        }



        public async Task AddClassification(ClassificationDto classificationDto)
        {
            var classification = new Classification { Name = classificationDto.Name,Title= classificationDto.Title };
            await _data.Classifications.AddAsync(classification);
        }

        public async Task<IEnumerable<ClassificationDto>> GetClassifications()
        {
            var classifications = await _data.Classifications.ToListAsync();

            var classificationsDtos = new List<ClassificationDto>();

            foreach(var c in classifications)
            {
                var classificatioDto = new ClassificationDto
                {
                    Title = c.Title,
                    Name = c.Name
                };

                classificationsDtos.Add(classificatioDto);
            }

            return classificationsDtos;
        }

        public void RemoveClassification(ClassificationDto classificationDto)
        {
            var classification = new Classification { Name = classificationDto.Name,Title= classificationDto.Title };
            _data.Classifications.Remove(classification);
        }

    }
}
