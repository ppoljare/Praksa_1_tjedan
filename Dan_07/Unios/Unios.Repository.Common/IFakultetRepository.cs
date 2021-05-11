using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model.Common;

namespace Unios.Repository.Common
{
    public interface IFakultetRepository
    {
        Task<IFakultetEntity> AddAsync(IFakultetEntity fakultet);
        Task<int> DeleteAsync(Guid id);
        Task<bool> FindAsync(Guid id);
        Task<List<IFakultetEntity>> GetAllAsync();
        Task<IFakultet> GetAsync(Guid id);
        Task<IFakultetEntity> UpdateAsync(IFakultetEntity fakultet);
    }
}
