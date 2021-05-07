using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;

namespace Unios.Service.Common
{
    public interface IStudentService
    {
        Task<int> Add(StudentInput student);
        Task<int> Delete(Guid id);
        Task<List<Student>> Get();
        Task<Student> Get(Guid id);
        Task<int> Update(Guid id, StudentInput student);
    }
}
