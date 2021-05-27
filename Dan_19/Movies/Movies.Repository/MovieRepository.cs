using AutoMapper;
using Movies.Common.Interfaces;
using Movies.Model.Common;
using Movies.Repository.Common;
using Movies.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Movies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly SqlConnection Connection = new SqlConnection(ConnectionString);
        private readonly IMapper Mapper;

        // CONSTRUCTOR
        public MovieRepository(IMapper mapper)
        {
            Mapper = mapper;
        }

        // POST
        public async Task<IMovie> AddAsync(IMovie movie)
        {
            var movieEntity = Mapper.Map<MovieEntity>(movie);

            string nonQueryString =
                "INSERT INTO Movie VALUES ('" +
                movieEntity.MovieId + "', '" +
                movieEntity.Name + "', '" +
                movieEntity.Genre + "', " +
                movieEntity.YearReleased + ")";

            SqlCommand command = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }

            Connection.Close();
            return movie;
        }

        // GET (COUNT)
        public async Task<int> CountAsync(IFilteringParams filteringParams)
        {
            string queryString =
                "SELECT COUNT(*) " +
                "FROM Movie";

            string filterString = filteringParams.GenerateFilteringString();
            queryString += filterString;

            SqlCommand command = new SqlCommand(queryString, Connection);
            await Connection.OpenAsync();
            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                int countRows = dataReader.GetInt32(0);
                Connection.Close();
                return countRows;
            }

            Connection.Close();
            return 0;
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id)
        {
            string nonQueryString =
                "DELETE FROM Movie " +
                "WHERE MovieId = '" + id + "'";

            SqlCommand command = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return false;
            }

            Connection.Close();
            return true;
        }

        // GET (ALL)
        public async Task<List<IMovie>> FindAsync(IFilteringParams filteringParams, ISortingParams sortingParams, IPaginationParams paginationParams)
        {
            int totalItems = await CountAsync(filteringParams);
            paginationParams.TotalItems = totalItems;

            List<IMovie> movies = new List<IMovie>();

            string queryString = "SELECT";
            queryString += paginationParams.GeneratePaginationString("start");

            queryString +=
                " MovieId, Name, Genre, YearReleased " +
                "FROM Movie";

            string filterString = filteringParams.GenerateFilteringString();
            queryString += filterString;

            string sortingString = sortingParams.GenerateSortingString();
            queryString += sortingString;

            queryString += paginationParams.GeneratePaginationString("end");

            SqlCommand command = new SqlCommand(queryString, Connection);
            await Connection.OpenAsync();
            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                MovieEntity movieEntity = new MovieEntity(
                    dataReader.GetGuid(0),
                    dataReader.GetString(1),
                    dataReader.GetString(2),
                    dataReader.GetInt32(3)
                );

                movies.Add(Mapper.Map<IMovie>(movieEntity));
            }

            Connection.Close();
            return movies;
        }

        // GET (BY ID)
        public async Task<IMovie> GetByIdAsync(Guid id)
        {
            string queryString =
               "SELECT MovieId, Name, Genre, YearReleased " +
               "FROM Movie " +
               "WHERE MovieId = '" + id + "'";

            SqlCommand command = new SqlCommand(queryString, Connection);
            await Connection.OpenAsync();
            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            if (dataReader.Read())
            {
                MovieEntity movieEntity = new MovieEntity(
                    dataReader.GetGuid(0),
                    dataReader.GetString(1),
                    dataReader.GetString(2),
                    dataReader.GetInt32(3)
                );
                Connection.Close();

                var movie = Mapper.Map<IMovie>(movieEntity);
                movie.Found = true;
                return movie;
            }
            else
            {
                Connection.Close();
                return null;
            }
        }

        // PUT
        public async Task<IMovie> UpdateAsync(IMovie movie)
        {
            var movieEntity = Mapper.Map<MovieEntity>(movie);

            string nonQueryString =
                "UPDATE Movie " +
                "SET Name = '" + movieEntity.Name + "', " +
                "Genre = '" + movieEntity.Genre + "', " +
                "YearReleased = " + movieEntity.YearReleased + " " +
                "WHERE MovieId = '" + movieEntity.MovieId + "'";

            SqlCommand command = new SqlCommand(nonQueryString, Connection);
            await Connection.OpenAsync();

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                Connection.Close();
                return null;
            }

            Connection.Close();
            movie.Found = true;
            return movie;
        }
    }
}
