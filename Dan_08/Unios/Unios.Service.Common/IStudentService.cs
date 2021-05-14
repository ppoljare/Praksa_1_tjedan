using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Common;
using Unios.Model.Common;

namespace Unios.Service.Common
{
    public interface IStudentService
    {
        Task<IStudent> AddAsync(IStudent student);
        Task<int> DeleteAsync(Guid id);
        Task<List<IStudent>> FindAsync(
            IStudentFilteringParams filteringParams,
            IStudentSortingParams sortingParams,
            IPaginationParams paginationParams
        );
        Task<IStudent> GetAsync(Guid id);
        Task<IStudent> UpdateAsync(IStudent student);
    }
}
