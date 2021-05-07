using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Repository.Common
{
    public interface IFakultetRepository
    {
        Task<int> Add(FakultetInput fakultet);
        Task<int> Delete(Guid id);
        Task<bool> Find(Guid id);
        Task<List<FakultetEntity>> GetAll();
        Task<Fakultet> Get(Guid id);
        Task<int> Update(Guid id, FakultetInput fakultet);
    }
}
