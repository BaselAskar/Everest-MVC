using everest.DTOs;
using everest.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface ISlideRepository
    {
        Task<List<SlideDto>> GetAllSlidesAsync();
        Task<Slide> FindSlideByIdAsync(string slideId);
        void AddSlide(Slide slide);

    }
}
