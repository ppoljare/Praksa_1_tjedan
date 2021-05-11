using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Unios.Model;
using Unios.Model.Common;
using Unios.Repository;
using Unios.Repository.Common;
using Unios.Service;
using Unios.Service.Common;

namespace Unios.WebApi
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Student>().As<IStudent>();
            builder.RegisterType<StudentEntity>().As<IStudentEntity>();
            builder.RegisterType<StudentInput>().As<IStudentInput>();
            builder.RegisterType<Fakultet>().As<IFakultet>();
            builder.RegisterType<FakultetEntity>().As<IFakultetEntity>();
            builder.RegisterType<FakultetInput>().As<IFakultetInput>();

            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<FakultetService>().As<IFakultetService>();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<FakultetRepository>().As<IFakultetRepository>();

            return builder.Build();
        }
    }
}