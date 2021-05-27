using Movies.Common.Interfaces;
using Movies.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Service.Common
{
    public interface IMovieService
    {
        Task<IMovie> AddAsync(IMovie movie);
        Task<bool> DeleteAsync(Guid id);
        Task<List<IMovie>> FindAsync(
            IFilteringParams filteringParams,
            ISortingParams sortingParams,
            IPaginationParams paginationParams
        );
        Task<IMovie> GetByIdAsync(Guid id);
        Task<IMovie> UpdateAsync(IMovie movie);
        void InitMovie(IMovie movie);
    }
}
