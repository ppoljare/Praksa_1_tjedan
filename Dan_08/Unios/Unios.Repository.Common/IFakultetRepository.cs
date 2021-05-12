using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model.Common;

namespace Unios.Repository.Common
{
    public interface IFakultetRepository
    {
        Task<IFakultet> AddAsync(IFakultet fakultet);
        Task<int> DeleteAsync(Guid id);
        Task<List<IFakultet>> FindAsync();
        Task<IFakultet> GetAsync(Guid id);
        Task<IFakultet> UpdateAsync(IFakultet fakultet);
    }
}
