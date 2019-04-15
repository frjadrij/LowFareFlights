using Autofac;
using LowFareFlights.Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFareFlights.DependencyInjection
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
        }
    }
}