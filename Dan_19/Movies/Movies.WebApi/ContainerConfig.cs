using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace Movies.WebApi
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new Modules.AutoMapperModule());
            builder.RegisterModule(new Service.Modules.ServiceModule());
            builder.RegisterModule(new Repository.Modules.RepositoryModule());

            return builder.Build();
        }
    }
}