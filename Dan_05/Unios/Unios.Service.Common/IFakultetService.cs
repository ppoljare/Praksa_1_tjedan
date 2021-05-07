using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Service.Common
{
    public interface IFakultetService
    {
        Task<int> AddAsync(FakultetInput fakultet);
        Task<int> DeleteAsync(Guid id);
        Task<List<FakultetEntity>> GetAsync();
        Task<Fakultet> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid id, FakultetInput fakultet);
    }
}
