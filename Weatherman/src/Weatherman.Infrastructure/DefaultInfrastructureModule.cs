using System.Reflection;
using Autofac;
using Weatherman.Core.Aggregates;
using Weatherman.Core.Interfaces;
using WeatherDDD.SharedKernel.Interfaces;
using Weatherman.Infrastructure.Data;
using Weatherman.Infrastructure.Messaging;
using IQueueService = Weatherman.Infrastructure.Messaging.IQueueService;
using Module = Autofac.Module;

namespace Weatherman.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly bool _isDevelopment;
        private readonly List<Assembly> _assemblies = [];

        public DefaultInfrastructureModule(bool isDevelopment, Assembly callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var coreAssembly = Assembly.GetAssembly(typeof(DailyTemperature));
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

            builder.RegisterType(typeof(QueueService))
                .As(typeof(IQueueService))
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(DailyTemperatureRepository))
                .As(typeof(IDailyTemperatureRepository))
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
