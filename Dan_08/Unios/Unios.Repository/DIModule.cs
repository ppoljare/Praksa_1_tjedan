using Autofac;
using Unios.Repository.Common;

namespace Unios.Repository
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<FakultetRepository>().As<IFakultetRepository>();
            base.Load(builder);
        }
    }
}
