using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using CraSampleApp.Domain.Domain.Service;
using CraSampleApp.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CraSampleApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Autofac.ContainerBuilder();
            container.RegisterType<ApplicationEfContext>().As<DbContext>().InstancePerRequest();
            container.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IEntityFrameworkRepository<>)).InstancePerLifetimeScope();
            container.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();
            container.RegisterType<MovieTypeService>().As<IMovieTypeService>().InstancePerRequest();
            container.RegisterControllers(Assembly.GetExecutingAssembly());
            container.RegisterType<FileLogger>().As<ILogger>().InstancePerRequest();


            var assembliesToScan = new List<Assembly> { typeof(ILogger).Assembly };
            var distinctAssemblies = assembliesToScan
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct()
                .ToArray();

            container.RegisterAssemblyTypes(distinctAssemblies)
                .AssignableTo(typeof(Profile))
                .As<Profile>()
                .SingleInstance();

            container
                .Register(componentContext => new MapperConfiguration(config => this.ConfigurationAction(config, componentContext)))
                .As<IConfigurationProvider>()
                .AsSelf()
                .SingleInstance();
            container
              .Register(componentContext => componentContext
                  .Resolve<MapperConfiguration>()
                  .CreateMapper(componentContext.Resolve<IComponentContext>().Resolve))
              .As<IMapper>()
              .InstancePerLifetimeScope();
            var builder = container.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder));

        }


        private void ConfigurationAction(IMapperConfigurationExpression cfg, IComponentContext componentContext)
        {



            var profiles = componentContext.Resolve<IEnumerable<Profile>>();

            foreach (var profile in profiles.Select(profile => profile.GetType()))
                cfg.AddProfile(profile);
        }
    }
}
