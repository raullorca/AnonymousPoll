using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using WebApi.Data;
using WebApi.Services;

namespace WebApi
{
    public class AutofacConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            // Register Web API controller in executing assembly.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<SurveyService>().As<ISurveyService>();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<SurveyRepository>().As<ISurveyRepository>();
            builder.RegisterType<Text2ListStudents>().As<IText2ListStudents>();
            builder.RegisterType<Text2ListSurvey>().As<IText2ListSurvey>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}