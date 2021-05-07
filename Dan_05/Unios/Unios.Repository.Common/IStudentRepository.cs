using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Repository.Common
{
    public interface IStudentRepository
    {
        Task<int> AddAsync(StudentInput student);
        Task<int> DeleteAsync(Guid id);
        Task<bool> FindAsync(Guid id);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid id, StudentInput student);
    }
}
