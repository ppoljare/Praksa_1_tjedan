using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;
using Unios.Repository;
using Unios.Service.Common;

namespace Unios.Service
{
    public class StudentService : IStudentService
    {
        protected StudentRepository Repository { get; private set; }
        
        public StudentService()
        {
            Repository = new StudentRepository();
        }

        public async Task<int> AddAsync(StudentInput student)
        {
            return await Repository.AddAsync(student);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if (await Repository.FindAsync(id) == false)
            {
                return -204;
            }
            return await Repository.DeleteAsync(id);
        }

        public async Task<List<Student>> GetAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<Student> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<int> UpdateAsync(Guid id, StudentInput student)
        {
            if (await Repository.FindAsync(id) == false)
            {
                return -404;
            }
            return await Repository.UpdateAsync(id, student);
        }
    }
}
