using AutoMapper;
using Unios.Model.Common;
using Unios.Repository.Entities;
using Unios.WebApi.Models;

namespace Unios.WebApi.Modules
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IFakultet, FakultetViewModel>();
            CreateMap<IFakultet, FakultetAndStudentsViewModel>();
            CreateMap<FakultetInputModel, IFakultet>();

            CreateMap<IStudent, StudentViewModel>();
            CreateMap<StudentInputModel, IStudent>();

            CreateMap<IStudent, StudentEntity>();
            CreateMap<StudentEntity, IStudent>();

            CreateMap<IFakultet, FakultetEntity>();
            CreateMap<FakultetEntity, IFakultet>();
        }
    }
}