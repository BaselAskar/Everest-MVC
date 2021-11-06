using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Interfaces
{
    public interface IUnitOfWork
    {
        IClassificationRepository ClassificationRepository { get; }

        IStoreRepository StoreRepository { get; }

        IClinicRepository ClinicRepository { get; }

        Task<bool> Complete();
    }
}
