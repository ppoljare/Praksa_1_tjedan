using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Unios.Model;
using Unios.Model.Common;

namespace Unios.WebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            builder.RegisterType<Student>().As<IStudent>();
            builder.RegisterType<StudentEntity>().As<IStudentEntity>();
            builder.RegisterType<StudentInput>().As<IStudentInput>();
            builder.RegisterType<Fakultet>().As<IFakultet>();
            builder.RegisterType<FakultetEntity>().As<IFakultetEntity>();
            builder.RegisterType<FakultetInput>().As<IFakultetInput>();


        }
    }
}