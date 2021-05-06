using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Unios.Model;

namespace Unios.Repository
{
    public class StudentRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);


        public int Add(StudentSQL student)
        {
            string queryString =
                "SELECT StudentID " +
                "FROM Student " +
                "WHERE (StudentID = '" + student.StudentID + "')";

            SqlCommand comm1 = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm1.ExecuteReader();

            if (dataReader.Read())
            {
                Connection.Close();
                return -403;
            }
            Connection.Close();

            string nonQueryString =
                "INSERT INTO Student VALUES ('" +
                student.StudentID + "', '" +
                student.FakultetID + "', '" +
                student.Ime + "', '" +
                student.Prezime + "')";

            SqlCommand comm2 = new SqlCommand(nonQueryString, Connection);
            Connection.Open();
            try
            {
                comm2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();

            return 0;
        }


        public int Delete(int id)
        {
            string idString = IdToString(id);

            string queryString =
                "SELECT StudentID " +
                "FROM Student " +
                "WHERE (StudentID = '" + idString + "')";

            SqlCommand comm1 = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm1.ExecuteReader();

            if (!dataReader.Read())
            {
                Connection.Close();
                return -204;
            }
            Connection.Close();

            string nonQueryString =
                "DELETE FROM Student " +
                "WHERE StudentID = '" + idString + "'";

            SqlCommand comm2 = new SqlCommand(nonQueryString, Connection);
            Connection.Open();
            try
            {
                comm2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();

            return 0;
        }


        public List<Student> GetAll()
        {
            List<Student> Storage = new List<Student>();

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID)";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm.ExecuteReader();

            while (dataReader.Read())
            {
                Student student = new Student (
                    dataReader[0].ToString(),
                    dataReader[1].ToString(),
                    dataReader[1].ToString(),
                    dataReader[2].ToString()
                );

                Storage.Add(student);
            }

            Connection.Close();

            return Storage;
        }


        public Student Get(int id)
        {
            Student student;

            string queryString =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID) " +
                "WHERE (StudentID = '" + IdToString(id) + "')";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm.ExecuteReader();

            if (dataReader.Read())
            {
                student = new Student(
                    dataReader[0].ToString(),
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


        public int Update(int id, StudentSQL student)
        {
            SetCorrectID(id, student);

            string queryString =
                "SELECT StudentID " +
                "FROM Student " +
                "WHERE (StudentID = '" + student.StudentID + "')";

            SqlCommand comm1 = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm1.ExecuteReader();

            if (!dataReader.Read())
            {
                Connection.Close();
                return -404;
            }
            Connection.Close();

            string nonQueryString =
                "UPDATE Student " +
                "SET FakultetID = '" + student.FakultetID + "', " +
                "Ime = '" + student.Ime + "', " +
                "Prezime = '" + student.Prezime + "' " +
                "WHERE StudentID = '" + student.StudentID + "'";

            SqlCommand comm2 = new SqlCommand(nonQueryString, Connection);
            Connection.Open();
            try
            {
                comm2.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Connection.Close();
                return -400;
            }

            Connection.Close();
            return 0;
        }


        private string IdToString(int id)
        {
            if (id < 1 || id > 999)
            {
                throw new ArgumentOutOfRangeException("ID must be between 1 and 999");
            }
            if (id < 10)
            {
                return "S00" + id.ToString();
            }
            if (id < 100)
            {
                return "S0" + id.ToString();
            }
            return "S" + id.ToString();
        }

        private void SetCorrectID(int id, StudentSQL student)
        {
            student.StudentID = IdToString(id);
        }
    }
}
