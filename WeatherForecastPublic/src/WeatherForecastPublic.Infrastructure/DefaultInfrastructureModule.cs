using System.Reflection;
using Autofac;
using WeatherForecastPublic.Core.Aggregates;
using WeatherForecastPublic.Core.Interfaces;
using WeatherDDD.SharedKernel.Interfaces;
using WeatherForecastPublic.Infrastructure.Data;
using Module = Autofac.Module;

namespace WeatherForecastPublic.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly bool _isDevelopment;
        private readonly List<Assembly> _assemblies = [];

        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var coreAssembly = Assembly.GetAssembly(typeof(DailyWeather));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(DefaultInfrastructureModule));
            _assemblies.Add(coreAssembly);
            _assemblies.Add(infrastructureAssembly);
            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private static void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>))
              .As(typeof(IRepository<>))
              .InstancePerLifetimeScope();

            builder.RegisterType(typeof(DailyWeatherRepository))
                .As(typeof(IDailyWeatherRepository))
                .InstancePerLifetimeScope();
        }

        private static void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private static void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }

    }
}
