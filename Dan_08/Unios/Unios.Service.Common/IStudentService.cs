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
        Task<int> CountAsync(StudentFilteringParams filteringParams);
        Task<List<IStudent>> FindAsync(
            StudentFilteringParams filteringParams,
            StudentSortingParams sortingParams,
            PaginationParams paginationParams
        );
        Task<IStudent> GetAsync(Guid id);
        Task<IStudent> UpdateAsync(IStudent student);
    }
}
