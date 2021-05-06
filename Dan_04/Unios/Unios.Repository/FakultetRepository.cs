using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Unios.Model;

namespace Unios.Repository
{
    public class FakultetRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);


        public int Add(FakultetSQL fakultet)
        {
            string queryString =
                "SELECT FakultetID " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + fakultet.FakultetID + "')";

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
                "INSERT INTO Fakultet VALUES ('" +
                fakultet.FakultetID + "', '" +
                fakultet.Naziv + "')";

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
                "SELECT FakultetID " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + idString + "')";

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
                "DELETE FROM Fakultet " +
                "WHERE FakultetID = '" + idString + "'";

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


        public List<FakultetSQL> GetAll()
        {
            List<FakultetSQL> Storage = new List<FakultetSQL>();
            
            string queryString =
                "SELECT FakultetID, Naziv " +
                "FROM Fakultet";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            Connection.Open();
            SqlDataReader dataReader = comm.ExecuteReader();

            while (dataReader.Read())
            {
                FakultetSQL fakultet = new FakultetSQL(
                    dataReader[0].ToString(),
                    dataReader[1].ToString()
                );

                Storage.Add(fakultet);
            }

            Connection.Close();

            return Storage;
        }


        public Fakultet Get(int id)
        {
            Fakultet fakultet;
            Student student;

            string queryString1 =
                "SELECT FakultetID, Naziv " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + IdToString(id) + "')";

            SqlCommand comm1 = new SqlCommand(queryString1, Connection);
            Connection.Open();
            SqlDataReader dataReader1 = comm1.ExecuteReader();

            if (dataReader1.Read())
            {
                fakultet = new Fakultet(dataReader1[0].ToString(), dataReader1[1].ToString());
            }
            else
            {
                Connection.Close();
                return null;
            }

            Connection.Close();

            string queryString2 =
                "SELECT StudentID, Ime, Prezime, Naziv " +
                "FROM Student JOIN Fakultet " +
                "ON (Student.FakultetID = Fakultet.FakultetID) " +
                "WHERE (Fakultet.FakultetID = '" + IdToString(id) + "')";

            SqlCommand comm2 = new SqlCommand(queryString2, Connection);

            Connection.Open();
            SqlDataReader dataReader2 = comm2.ExecuteReader();

            while (dataReader2.Read())
            {
                student = new Student(
                    dataReader2[0].ToString(),
                    dataReader2[1].ToString(),
                    dataReader2[2].ToString(),
                    dataReader2[3].ToString()
                );

                fakultet.Add(student);
            }

            Connection.Close();
            return fakultet;
        }


        public int Update(int id, FakultetSQL fakultet)
        {
            SetCorrectID(id, fakultet);

            string queryString =
                "SELECT FakultetID " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + fakultet.FakultetID + "')";

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
                "UPDATE Fakultet " +
                "SET Naziv = '" + fakultet.Naziv + "' " +
                "WHERE FakultetID = '" + fakultet.FakultetID + "'";

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


        public string IdToString(int id)
        {
            if (id < 1 || id > 999)
            {
                throw new ArgumentOutOfRangeException("ID must be between 1 and 999");
            }
            if (id < 10)
            {
                return "F00" + id.ToString();
            }
            if (id < 100)
            {
                return "F0" + id.ToString();
            }
            return "F" + id.ToString();
        }

        private void SetCorrectID(int id, FakultetSQL fakultet)
        {
            fakultet.FakultetID = IdToString(id);
        }
    }
}
