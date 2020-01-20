using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MemoryQueue.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers handlers and Queue types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>        
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, params Assembly[] assemblies)
            => services.AddMemoryQueue(assemblies, configuration: null);

        /// <summary>
        /// Registers handlers and Queue types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, Action<MemoryQueueServiceConfiguration> configuration, params Assembly[] assemblies)
            => services.AddMemoryQueue(assemblies, configuration);

        /// <summary>
        /// Registers handlers and Queue types from the specified assemblies
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<MemoryQueueServiceConfiguration> configuration)
        {
            if (!assemblies.Any())
            {
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
            }
            var serviceConfig = new MemoryQueueServiceConfiguration();

            configuration?.Invoke(serviceConfig);

            ServiceRegistrar.AddRequiredServices(services, serviceConfig);

            ServiceRegistrar.AddMemoryQueueClasses(services, assemblies);

            return services;
        }

        /// <summary>
        /// Registers handlers and Queue types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>        
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMemoryQueue(handlerAssemblyMarkerTypes, configuration: null);

        /// <summary>
        /// Registers handlers and Queue types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, Action<MemoryQueueServiceConfiguration> configuration, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMemoryQueue(handlerAssemblyMarkerTypes, configuration);

        /// <summary>
        /// Registers handlers and Queue types from the assemblies that contain the specified types
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <param name="configuration">The action used to configure the options</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, IEnumerable<Type> handlerAssemblyMarkerTypes, Action<MemoryQueueServiceConfiguration> configuration)
            => services.AddMemoryQueue(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), configuration);
    }
}
