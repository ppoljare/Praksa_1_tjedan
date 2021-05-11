using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Model;
using Unios.Model.Common;
using Unios.Repository.Common;
using Unios.Service.Common;

namespace Unios.Service
{
    public class StudentService : IStudentService
    {
        protected IStudentRepository Repository { get; private set; }
        
        public StudentService(IStudentRepository repository)
        {
            Repository = repository;
        }

        public async Task<IStudentEntity> AddAsync(StudentInput input)
        {
            IStudentEntity student = new StudentEntity(input.Ime, input.Prezime, input.FakultetID);
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

        public async Task<List<IStudent>> GetAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<IStudent> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<IStudentEntity> UpdateAsync(Guid id, StudentInput input)
        {
            if (await Repository.FindAsync(id) == false)
            {
                return new StudentEntity(id, null, null, input.FakultetID);
            }

            IStudentEntity student = new StudentEntity(id, input.Ime, input.Prezime, input.FakultetID);
            return await Repository.UpdateAsync(student);
        }
    }
}
