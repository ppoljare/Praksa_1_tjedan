using Autofac;
using Movies.Repository.Common;

namespace Movies.Repository.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
        }
    }
}
