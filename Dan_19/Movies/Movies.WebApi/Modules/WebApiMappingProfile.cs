using AutoMapper;
using Movies.Model.Common;
using Movies.WebApi.Models.Movie;

namespace Movies.WebApi.Modules
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<IMovie, MovieViewModel>().ReverseMap();
            CreateMap<MovieInputModel, IMovie>();
        }
    }
}