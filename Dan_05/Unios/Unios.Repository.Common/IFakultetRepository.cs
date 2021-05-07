using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Repository.Common
{
    public interface IFakultetRepository
    {
        Task<int> AddAsync(FakultetInput fakultet);
        Task<int> DeleteAsync(Guid id);
        Task<bool> FindAsync(Guid id);
        Task<List<FakultetEntity>> GetAllAsync();
        Task<Fakultet> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid id, FakultetInput fakultet);
    }
}
