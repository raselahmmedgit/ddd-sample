using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using App.Data;
using App.Service;
using App.Domain;
using App.DomainViewModel;

namespace App.Web
{
    public static class BootStrapper
    {
        public static void Run()
        {
            SetIocContainer();
        }

        private static void SetIocContainer()
        {
            try
            {
                //Implement Autofac

                var configuration = GlobalConfiguration.Configuration;
                var builder = new ContainerBuilder();

                // Register MVC controllers using assembly scanning.
                builder.RegisterControllers(Assembly.GetExecutingAssembly());

                // Register MVC controller and API controller dependencies per request.
                builder.Register(c => new dbEntities()).InstancePerLifetimeScope();
                builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
                builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
                builder.RegisterGeneric(typeof(Repository<>)).InstancePerLifetimeScope();

                // Register service
                builder.RegisterAssemblyTypes(typeof(CategoryService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerDependency();

                var container = builder.Build();

                //for MVC Controller Set the dependency resolver implementation.
                var resolverMvc = new AutofacDependencyResolver(container);
                System.Web.Mvc.DependencyResolver.SetResolver(resolverMvc);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}