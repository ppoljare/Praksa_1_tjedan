using AutoMapper;
using Movies.Model.Common;
using Movies.Repository.Entities;

namespace Movies.Repository.Modules
{
    public class RepositoryMappingProfile : Profile
    {
        public RepositoryMappingProfile()
        {
            CreateMap<IMovie, MovieEntity>().ReverseMap();
        }
    }
}
