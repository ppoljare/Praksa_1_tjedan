using Movies.Model.Common;
using System;

namespace Movies.Model
{
    class Movie : IMovie
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int YearReleased { get; set; }
        public bool Found { get; set; }
    }
}
