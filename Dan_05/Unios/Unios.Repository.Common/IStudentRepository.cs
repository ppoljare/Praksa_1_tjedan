using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Repository.Common
{
    public interface IStudentRepository
    {
        Task<int> Add(StudentInput student);
        Task<int> Delete(Guid id);
        Task<bool> Find(Guid id);
        Task<List<Student>> GetAll();
        Task<Student> Get(Guid id);
        Task<int> Update(Guid id, StudentInput student);
    }
}
