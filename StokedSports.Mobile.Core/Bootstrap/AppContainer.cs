using System;
using Autofac;
using StokedSports.Mobile.Core.Repository;
using StokedSports.Mobile.Core.Services.General;
using StokedSports.Mobile.Core.ViewModels;

namespace StokedSports.Mobile.Core.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<SimpleLoginPageModel>();

            //services - data

            //services - general
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
