using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using PadariaExpress.IOC;
using PadariaExpress.Website.Controllers;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.WebApi;
using PadariaExpress.Website.AutoMapper;

namespace PadariaExpress.Website
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = RegistradorDeDependencias.GetContainer();
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            Registration registration = container.GetRegistration(typeof(UsuarioController)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Teste");

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutoMapperConfig.RegisterMappings();

        }
    }
}
