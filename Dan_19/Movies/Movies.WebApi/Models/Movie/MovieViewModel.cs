using System;

namespace Movies.WebApi.Models.Movie
{
    public class MovieViewModel
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int YearReleased { get; set; }
    }
}