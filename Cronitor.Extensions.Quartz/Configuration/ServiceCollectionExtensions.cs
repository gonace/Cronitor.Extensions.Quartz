﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace Cronitor.Extensions.Quartz.Configuration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds OpenTracing instrumentation for Quartz.
        /// </summary>
        public static IServiceCollection AddQuartzOpenTracing(this IServiceCollection services, Action<QuartzDiagnosticOptions>? options = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (options != null)
                services.Configure(options);

            //services.TryAddSingleton<ITracer>(GlobalTracer.Instance);
            services.TryAddSingleton<QuartzDiagnostic>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHostedService, InstrumentationService>());

            return services;
        }
    }
}
