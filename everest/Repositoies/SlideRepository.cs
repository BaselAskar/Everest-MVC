using AutoMapper;
using everest.Data;
using everest.DTOs;
using everest.Entities;
using everest.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Repositoies
{
    public class SlideRepository : ISlideRepository
    {

        private readonly DataContext _data;
        private readonly IMapper _mapper;

        public SlideRepository(DataContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        public void AddSlide(Slide slide)
        {
            _data.Slides.Add(slide);
        }

        public async Task<Slide> FindSlideByIdAsync(string slideId)
        {
            return await _data.Slides
                .Include(slide => slide.Store)
                .Include(slide => slide.Photos)
                .SingleOrDefaultAsync(slide => slide.Id == slideId);
                            
        }

        public Task<List<SlideDto>> GetAllSlidesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
