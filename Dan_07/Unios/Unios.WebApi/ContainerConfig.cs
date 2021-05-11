using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

namespace Unios.WebApi
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<Service.DIModule>();
            builder.RegisterModule<Repository.DIModule>();

            return builder.Build();
        }
    }
}