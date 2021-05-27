using Autofac;
using AutoMapper;
using Movies.Repository.Modules;

namespace Movies.WebApi.Modules
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new MapperConfiguration(cfg => {
                cfg.AddProfile<WebApiMappingProfile>();
                cfg.AddProfile<RepositoryMappingProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}