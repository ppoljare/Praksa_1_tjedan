using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;
using Unios.Model.Common;

namespace Unios.Service.Common
{
    public interface IStudentService
    {
        Task<IStudentEntity> AddAsync(StudentInput student);
        Task<int> DeleteAsync(Guid id);
        Task<List<IStudent>> GetAsync();
        Task<IStudent> GetAsync(Guid id);
        Task<IStudentEntity> UpdateAsync(Guid id, StudentInput student);
    }
}
