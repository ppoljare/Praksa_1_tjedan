using System;

namespace Movies.Repository.Entities
{
    public class MovieEntity
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int YearReleased { get; set; }

        public MovieEntity() { }

        public MovieEntity(Guid id, string name, string genre, int yearReleased)
        {
            MovieId = id;
            Name = name;
            Genre = genre;
            YearReleased = yearReleased;
        }
    }
}
