using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model.Common;

namespace Unios.Repository.Common
{
    public interface IStudentRepository
    {
        Task<IStudentEntity> AddAsync(IStudentEntity student);
        Task<int> DeleteAsync(Guid id);
        Task<bool> FindAsync(Guid id);
        Task<List<IStudent>> GetAllAsync();
        Task<IStudent> GetAsync(Guid id);
        Task<IStudentEntity> UpdateAsync(IStudentEntity student);
    }
}
