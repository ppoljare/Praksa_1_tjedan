using System;

namespace Movies.Model.Common
{
    public interface IMovie
    {
        Guid MovieId { get; set; }
        string Name { get; set; }
        string Genre { get; set; }
        int YearReleased { get; set; }
        bool Found { get; set; }
    }
}
