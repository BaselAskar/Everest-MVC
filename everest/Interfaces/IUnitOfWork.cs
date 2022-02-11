using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IUnitOfWork
    {

        IStoreClassificationRepository StoreClassificationRepository { get; }

        IStoreRepository StoreRepository { get; }

        IClinicRepository ClinicRepository { get; }

        ISlideRepository SlideRepository { get; }

        Task<bool> Complete();
    }
}
