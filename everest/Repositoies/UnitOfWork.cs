using AutoMapper;
using everest.Data;
using everest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Repositoies
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _data;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }


        public IStoreRepository StoreRepository => new StoreRepository(_data,_mapper);

        public IClinicRepository ClinicRepository => new ClinicRepository(_data);

        public ISlideRepository SlideRepository => new SlideRepository(_data, _mapper);

        public IStoreClassificationRepository StoreClassificationRepository => new StoreClassificatioRepository(_data, _mapper);

        public async Task<bool> Complete()
        {
            return await _data.SaveChangesAsync() > 0;
        }
    }
}
