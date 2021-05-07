using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Service.Common
{
    public interface IStudentService
    {
        Task<int> AddAsync(StudentInput student);
        Task<int> DeleteAsync(Guid id);
        Task<List<Student>> GetAsync();
        Task<Student> GetAsync(Guid id);
        Task<int> UpdateAsync(Guid id, StudentInput student);
    }
}
