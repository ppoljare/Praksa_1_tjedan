using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieAPI.Models
{
    public class MovieRepository : IMovieRepository
    {
        private List<Movie> _movies = new List<Movie>();
        private int _nextID = 1;

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie Get(int id)
        {
            return _movies.Find(p => p.Id == id);
        }

        public Movie Add(MovieJson movieJson)
        {
            if (movieJson == null)
            {
                throw new ArgumentNullException("movie");
            }
            Movie movie = JsonToMovie(movieJson, _nextID++);
            _movies.Add(movie);
            return movie;
        }

        public void Remove(int id)
        {
            _movies.RemoveAll(p => p.Id == id);
        }

        public bool Update(int id, MovieJson movieJson)
        {
            if (movieJson == null)
            {
                throw new ArgumentNullException("movie");
            }
            int index = _movies.FindIndex(p => p.Id == id);
            if (index == -1)
            {
                return false;
            }
            _movies.RemoveAt(index);
            Movie movie = JsonToMovie(movieJson, id);
            _movies.Insert(index, movie);
            return true;
        }

        public Movie JsonToMovie(MovieJson movieJson, int id)
        {
            Movie movie = new Movie
            {
                Id = id,
                Name = movieJson.Name,
                Year = movieJson.Year,
                Genre = movieJson.Genre
            };

            return movie;
        }
    }
}