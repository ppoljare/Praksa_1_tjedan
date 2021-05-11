using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;
using Unios.Model.Common;

namespace Unios.Service.Common
{
    public interface IFakultetService
    {
        Task<IFakultetEntity> AddAsync(FakultetInput fakultet);
        Task<int> DeleteAsync(Guid id);
        Task<List<IFakultetEntity>> GetAsync();
        Task<IFakultet> GetAsync(Guid id);
        Task<IFakultetEntity> UpdateAsync(Guid id, FakultetInput fakultet);
    }
}
