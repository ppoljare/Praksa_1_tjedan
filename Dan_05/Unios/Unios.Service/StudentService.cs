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

        public async Task<int> Add(StudentInput student)
        {
            return await Repository.Add(student);
        }

        public async Task<int> Delete(Guid id)
        {
            if (await Repository.Find(id) == false)
            {
                return -204;
            }
            return await Repository.Delete(id);
        }

        public async Task<List<Student>> Get()
        {
            return await Repository.GetAll();
        }

        public async Task<Student> Get(Guid id)
        {
            return await Repository.Get(id);
        }

        public async Task<int> Update(Guid id, StudentInput student)
        {
            if (await Repository.Find(id) == false)
            {
                return -404;
            }
            return await Repository.Update(id, student);
        }
    }
}
