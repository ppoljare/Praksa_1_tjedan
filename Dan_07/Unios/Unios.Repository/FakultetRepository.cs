using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Unios.Model;
using Unios.Model.Common;
using Unios.Repository.Common;

namespace Unios.Repository
{
    public class FakultetRepository : IFakultetRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);


        public async Task<IFakultetEntity> AddAsync(IFakultetEntity fakultet)
        {
            string nonQueryString =
                "INSERT INTO Fakultet VALUES ('" +
                fakultet.FakultetID + "', '" +
                fakultet.Naziv + "')";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }

            Connection.Close();

            return fakultet;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            string nonQueryString =
                "DELETE FROM Fakultet " +
                "WHERE FakultetID = '" + id + "'";

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
                "SELECT FakultetID " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + id + "')";

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


        public async Task<List<IFakultetEntity>> GetAllAsync()
        {
            List<IFakultetEntity> storage = new List<IFakultetEntity>();
            
            string queryString =
                "SELECT FakultetID, Naziv " +
                "FROM Fakultet";

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                IFakultetEntity fakultet = new FakultetEntity(
                    Guid.Parse(dataReader[0].ToString()),
                    dataReader[1].ToString()
                );

                storage.Add(fakultet);
            }

            Connection.Close();

            return storage;
        }


        public async Task<IFakultet> GetAsync(Guid id)
        {
            Fakultet fakultet;
            Student student;

            string queryString1 =
                "SELECT FakultetID, Naziv " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + id + "')";

            SqlCommand comm1 = new SqlCommand(queryString1, Connection);
            await Connection.OpenAsync();
            SqlDataReader dataReader1 = await comm1.ExecuteReaderAsync();

            if (dataReader1.Read())
            {
                fakultet = new Fakultet(Guid.Parse(dataReader1[0].ToString()), dataReader1[1].ToString());
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
                "WHERE (Fakultet.FakultetID = '" + id + "')";

            SqlCommand comm2 = new SqlCommand(queryString2, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader2 = await comm2.ExecuteReaderAsync();

            while (dataReader2.Read())
            {
                student = new Student(
                    Guid.Parse(dataReader2[0].ToString()),
                    dataReader2[1].ToString(),
                    dataReader2[2].ToString(),
                    dataReader2[3].ToString()
                );

                fakultet.Add(student);
            }

            Connection.Close();
            return fakultet;
        }


        public async Task<IFakultetEntity> UpdateAsync(IFakultetEntity fakultet)
        {
            string nonQueryString =
                "UPDATE Fakultet " +
                "SET Naziv = '" + fakultet.Naziv + "' " +
                "WHERE FakultetID = '" + fakultet.FakultetID + "'";

            SqlCommand comm = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();
            try
            {
                await comm.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }

            Connection.Close();
            return fakultet;
        }
    }
}
