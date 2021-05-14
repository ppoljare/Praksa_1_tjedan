using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Unios.Common;
using Unios.Model.Common;
using Unios.Repository.Common;
using Unios.Repository.Entities;

namespace Unios.Repository
{
    public class FakultetRepository : IFakultetRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(_connectionString);
        private readonly IMapper Mapper;

        public FakultetRepository(IMapper mapper)
        {
            Mapper = mapper;
        }


        public async Task<IFakultet> AddAsync(IFakultet fakultet)
        {
            var fakultetEntity = Mapper.Map<FakultetEntity>(fakultet);

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


        public async Task<int> CountAsync(IFakultetFilteringParams filteringParams)
        {
            string queryString =
                "SELECT COUNT(*) " +
                "FROM Fakultet";

            string filterString = GenerateFilterString(filteringParams);
            queryString += filterString;

            SqlCommand comm = new SqlCommand(queryString, Connection);

            await Connection.OpenAsync();
            SqlDataReader dataReader = await comm.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                int countRows = int.Parse(dataReader[0].ToString());
                Connection.Close();
                return countRows;
            }

            Connection.Close();
            return 0;
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


        public async Task<List<IFakultet>> FindAsync(
            IFakultetFilteringParams filteringParams,
            IFakultetSortingParams sortingParams,
            IPaginationParams paginationParams
        )
        {
            List<FakultetEntity> storage = new List<FakultetEntity>();

            string queryString = "SELECT";
            queryString += paginationParams.GeneratePaginationString("start");

            queryString +=
                " FakultetID, Naziv, Vrsta " +
                "FROM Fakultet";

            string filterString = GenerateFilterString(filteringParams);
            queryString += filterString;
            queryString += " ORDER BY " + sortingParams.SortBy + " " + sortingParams.SortOrder.ToUpper();

            string paginationString = paginationParams.GeneratePaginationString("end");
            queryString += paginationString;

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
            var result = Mapper.Map<List<IFakultet>>(storage);
            return result;
        }


        public async Task<IFakultet> GetAsync(Guid id)
        {
            FakultetEntity fakultetEntity;
            
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

            var fakultet = Mapper.Map<IFakultet>(fakultetEntity);
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
                StudentEntity studentEntity = new StudentEntity(
                    Guid.Parse(dataReader2[0].ToString()),
                    dataReader2[1].ToString(),
                    dataReader2[2].ToString(),
                    dataReader2[3].ToString(),
                    int.Parse(dataReader2[4].ToString())
                );

                fakultet.Studenti.Add(Mapper.Map<IStudent>(studentEntity));
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


        private string GenerateFilterString(IFakultetFilteringParams filteringParams)
        {
            string filterString = "";
            int counter = 0;

            if (filteringParams.Naziv != null)
            {
                counter++;
                filterString = " WHERE ";
                filterString += "LOWER(Naziv) LIKE '%" + filteringParams.Naziv.ToLower() + "%'";
            }

            if (filteringParams.Vrsta != null)
            {
                if (counter == 0)
                {
                    filterString = " WHERE ";
                }
                else
                {
                    filterString += " AND ";
                }
                filterString += "LOWER(Vrsta) LIKE '%" + filteringParams.Vrsta.ToLower() + "%'";
            }

            return filterString;
        }
    }
}
