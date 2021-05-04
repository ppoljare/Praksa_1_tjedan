using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie Get(int id);
        Movie Add(MovieJson movieJson);
        void Remove(int id);
        bool Update(int id, MovieJson movieJson);
    }
}
