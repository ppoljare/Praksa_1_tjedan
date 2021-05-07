using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Unios.Model;
using Unios.Repository.Common;
using System.Threading.Tasks;

namespace Unios.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);


        public async Task<int> AddAsync(StudentInput student)
        {
            string nonQueryString =
                "INSERT INTO Student (FakultetID, Ime, Prezime) VALUES ('" +
                student.FakultetID + "', '" +
                student.Ime + "', '" +
                student.Prezime + "')";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();

            return 0;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            string nonQueryString =
                "DELETE FROM Student " +
                "WHERE StudentID = '" + id + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();

            return 0;
        }


        public async Task<bool> FindAsync(Guid id)
        {
            string queryString =
                "SELECT StudentID " +
                "FROM Student " +
                "WHERE (StudentID = '" + id + "')";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                Connection.Close();
                return true;
            }
            Connection.Close();
            return false;
        }


        public async Task<List<Student>> GetAllAsync()
        {
            List<Student> storage = new List<Student>();

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID)";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                Student student = new Student (
                    Guid.Parse(dataReader[0].ToString()),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString()
                );

                storage.Add(student);
            }

            Connection.Close();

            return storage;
        }


        public async Task<Student> GetAsync(Guid id)
        {
            Student student;

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID) " +
                "WHERE (StudentID = '" + id + "')";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                student = new Student(
                    Guid.Parse(dataReader[0].ToString()),
                    dataReader[1].ToString(),
                    dataReader[2].ToString(),
                    dataReader[3].ToString()
                );

                Connection.Close();
                return student;
            }
            else
            {
                Connection.Close();
                return null;
            }
        }


        public async Task<int> UpdateAsync(Guid id, StudentInput student)
        {
            string nonQueryString =
                "UPDATE Student " +
                "SET FakultetID = '" + student.FakultetID + "', " +
                "Ime = '" + student.Ime + "', " +
                "Prezime = '" + student.Prezime + "' " +
                "WHERE StudentID = '" + id + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();
            return 0;
        }
    }
}
