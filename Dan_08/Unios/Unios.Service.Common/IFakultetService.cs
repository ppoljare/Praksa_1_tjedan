using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Common;
using Unios.Model.Common;

namespace Unios.Service.Common
{
    public interface IFakultetService
    {
        Task<IFakultet> AddAsync(IFakultet fakultet);
        Task<int> DeleteAsync(Guid id);
        Task<List<IFakultet>> FindAsync(
            IFakultetFilteringParams filteringParams,
            IFakultetSortingParams sortingParams,
            IPaginationParams paginationParams
        );
        Task<IFakultet> GetAsync(Guid id);
        Task<IFakultet> UpdateAsync(IFakultet fakultet);
    }
}
