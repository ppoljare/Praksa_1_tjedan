using Movies.Common.Interfaces;
using Movies.Model.Common;
using Movies.Repository.Common;
using Movies.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Service
{
    public class MovieService : IMovieService
    {
        protected IMovieRepository Repository { get; private set; }

        public MovieService(IMovieRepository repository)
        {
            Repository = repository;
        }

        public async Task<IMovie> AddAsync(IMovie movie)
        {
            if (!IsDataValid(movie))
            {
                return null;
            }

            if (await Repository.GetByIdAsync(movie.MovieId) == null)
            {
                movie.Found = false;
                return await Repository.AddAsync(movie);
            }
            movie.Found = true;
            return movie;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (await Repository.GetByIdAsync(id) == null)
            {
                return false;
            }
            return await Repository.DeleteAsync(id);
        }

        public async Task<List<IMovie>> FindAsync(IFilteringParams filteringParams, ISortingParams sortingParams, IPaginationParams paginationParams)
        {
            return await Repository.FindAsync(filteringParams, sortingParams, paginationParams);
        }

        public async Task<IMovie> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public async Task<IMovie> UpdateAsync(IMovie movie)
        {
            if (!IsDataValid(movie))
            {
                return null;
            }

            if (await Repository.GetByIdAsync(movie.MovieId) == null)
            {
                movie.Found = false;
                return movie;
            }

            return await Repository.UpdateAsync(movie);
        }

        public void InitMovie(IMovie movie)
        {
            movie.MovieId = Guid.NewGuid();
        }

        private bool IsDataValid(IMovie movie)
        {
            if (movie.Name.Length < 1 || movie.Name.Length > 50)
            {
                return false;
            }

            if (movie.Genre.Length < 1 || movie.Genre.Length > 50)
            {
                return false;
            }

            if (movie.YearReleased <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
