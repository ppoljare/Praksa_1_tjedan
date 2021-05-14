using Autofac;
using Unios.Repository.Common;

namespace Unios.Repository.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<FakultetRepository>().As<IFakultetRepository>();
        }
    }
}
