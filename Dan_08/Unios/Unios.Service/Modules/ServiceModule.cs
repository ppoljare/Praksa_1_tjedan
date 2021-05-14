using Autofac;
using Unios.Service.Common;

namespace Unios.Service.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<FakultetService>().As<IFakultetService>();
            base.Load(builder);
        }
    }
}
