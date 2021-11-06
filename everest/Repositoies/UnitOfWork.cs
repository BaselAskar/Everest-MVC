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

        public IClassificationRepository ClassificationRepository => new ClassificationRepository(_data, _mapper);

        public IStoreRepository StoreRepository => new StoreRepository(_data);

        public IClinicRepository ClinicRepository => new ClinicRepository(_data);

        public async Task<bool> Complete()
        {
            return await _data.SaveChangesAsync() > 0;
        }
    }
}
