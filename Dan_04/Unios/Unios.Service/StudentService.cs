using System.Collections.Generic;
using Unios.Model;
using Unios.Repository;

namespace Unios.Service
{
    public class StudentService
    {
        protected StudentRepository Repository { get; private set; }
        
        public StudentService()
        {
            Repository = new StudentRepository();
        }

        public int Add(StudentSQL student)
        {
            return Repository.Add(student);
        }

        public int Delete(int id)
        {
            return Repository.Delete(id);
        }

        public List<Student> Get()
        {
            return Repository.GetAll();
        }

        public Student Get(int id)
        {
            return Repository.Get(id);
        }

        public int Update(int id, StudentSQL student)
        {
            return Repository.Update(id, student);
        }
    }
}
