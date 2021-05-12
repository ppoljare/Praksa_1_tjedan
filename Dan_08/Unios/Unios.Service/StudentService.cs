using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unios.Common;
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

        public async Task<IStudent> AddAsync(IStudent student)
        {
            if(await Repository.GetAsync(student.StudentID) == null)
            {
                return await Repository.AddAsync(student);
            }
            return null;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if (await Repository.GetAsync(id) == null)
            {
                return -204;
            }
            return await Repository.DeleteAsync(id);
        }

        public async Task<List<IStudent>> FindAsync(StudentSortingParams sortingParams)
        {
            return await Repository.FindAsync(sortingParams);
        }

        public async Task<IStudent> GetAsync(Guid id)
        {
            return await Repository.GetAsync(id);
        }

        public async Task<IStudent> UpdateAsync(IStudent student)
        {
            if (await Repository.GetAsync(student.StudentID) == null)
            {
                student.Found = false;
                return student;
            }

            return await Repository.UpdateAsync(student);
        }
    }
}
