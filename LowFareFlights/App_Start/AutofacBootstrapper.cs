using Autofac;
using Autofac.Integration.Mvc;
using LowFareFlights.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LowFareFlights.App_Start
{
    public class AutofacBootstrapper
    {
        public static void Run()
        {
            SetAutofacMvc();
        }

        public static void SetAutofacMvc()
        {
            var builder = new ContainerBuilder();

            // Register MVC controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //Register Dependencies
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new UnitOfWorkModule());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}