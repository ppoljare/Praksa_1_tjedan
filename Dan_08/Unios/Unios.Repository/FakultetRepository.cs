using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Unios.Common;
using Unios.Model;
using Unios.Model.Common;
using Unios.Repository.Common;
using Unios.Repository.Entities;

namespace Unios.Repository
{
    public class FakultetRepository : IFakultetRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);


        public async Task<IFakultet> AddAsync(IFakultet fakultet)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IFakultet, FakultetEntity>()
            );
            var mapper = new Mapper(config);
            var fakultetEntity = mapper.Map<FakultetEntity>(fakultet);

            string nonQueryString =
                "INSERT INTO Fakultet VALUES ('" +
                fakultetEntity.FakultetID + "', '" +
                fakultetEntity.Naziv + "', '" +
                fakultetEntity.Vrsta + "')";

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


        public async Task<List<IFakultet>> FindAsync(FakultetSortingParams sortingParams)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<FakultetEntity, IFakultet>()
            );
            var mapper = new Mapper(config);
            
            List<FakultetEntity> storage = new List<FakultetEntity>();
            
            string queryString =
                "SELECT FakultetID, Naziv, Vrsta " +
                "FROM Fakultet";

            if (!sortingParams.IsNull())
            {
                queryString += " ORDER BY " + sortingParams.SortBy + " " + sortingParams.SortOrder.ToUpper();
            }

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                FakultetEntity fakultetEntity = new FakultetEntity(
                    Guid.Parse(dataReader[0].ToString()),
                    dataReader[1].ToString(),
                    dataReader[2].ToString()
                );

                storage.Add(fakultetEntity);
            }

            Connection.Close();
            var result = mapper.Map<List<IFakultet>>(storage);
            return result;
        }


        public async Task<IFakultet> GetAsync(Guid id)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<FakultetEntity, IFakultet>()
            );
            var mapper = new Mapper(config);

            FakultetEntity fakultetEntity;
            IStudent student;

            string queryString1 =
                "SELECT FakultetID, Naziv, Vrsta " +
                "FROM Fakultet " +
                "WHERE (FakultetID = '" + id + "')";

            SqlCommand comm1 = new SqlCommand(queryString1, Connection);
            await Connection.OpenAsync();
            SqlDataReader dataReader1 = await comm1.ExecuteReaderAsync();

            if (dataReader1.Read())
            {
                fakultetEntity = new FakultetEntity(
                    Guid.Parse(dataReader1[0].ToString()),
                    dataReader1[1].ToString(),
                    dataReader1[2].ToString()
                );
            }
            else
            {
                Connection.Close();
                return null;
            }

            Connection.Close();

            var fakultet = mapper.Map<IFakultet>(fakultetEntity);
            fakultet.Studenti = new List<IStudent>();

            string queryString2 =
                "SELECT StudentID, Ime, Prezime, Naziv, Godina " +
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
                    dataReader2[3].ToString(),
                    int.Parse(dataReader2[4].ToString())
                );

                fakultet.Studenti.Add(student);
            }

            Connection.Close();
            return fakultet;
        }


        public async Task<IFakultet> UpdateAsync(IFakultet fakultet)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<IFakultet, FakultetEntity>()
            );
            var mapper = new Mapper(config);
            var fakultetEntity = mapper.Map<FakultetEntity>(fakultet);

            string nonQueryString =
                "UPDATE Fakultet " +
                "SET Naziv = '" + fakultetEntity.Naziv + "', " +
                "Vrsta = '" + fakultetEntity.Vrsta + "' " +
                "WHERE FakultetID = '" + fakultetEntity.FakultetID + "'";

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
            fakultet.Found = true;
            return fakultet;
        }
    }
}
