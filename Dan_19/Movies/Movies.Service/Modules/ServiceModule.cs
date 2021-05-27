using Autofac;
using Movies.Service.Common;

namespace Movies.Service.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MovieService>().As<IMovieService>();
        }
    }
}
