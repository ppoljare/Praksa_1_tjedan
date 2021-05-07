using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Service.Common
{
    public interface IFakultetService
    {
        Task<int> Add(FakultetInput fakultet);
        Task<int> Delete(Guid id);
        Task<List<FakultetEntity>> Get();
        Task<Fakultet> Get(Guid id);
        Task<int> Update(Guid id, FakultetInput fakultet);
    }
}
