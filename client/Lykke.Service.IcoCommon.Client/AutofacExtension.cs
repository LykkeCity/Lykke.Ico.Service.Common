using System;
using Autofac;
using Common.Log;

namespace Lykke.Service.IcoCommon.Client
{
    public static class AutofacExtension
    {
        public static void RegisterIcoCommonClient(this ContainerBuilder builder, string serviceUrl, ILog log)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<IcoCommonAPI>()
                .WithParameter(TypedParameter.From(new Uri(serviceUrl)))
                .As<IIcoCommonAPI>()
                .SingleInstance();

            builder.RegisterType<IcoCommonServiceClient>()
                .As<IIcoCommonServiceClient>()
                .SingleInstance();
        }

        public static void RegisterIcoCommonClient(this ContainerBuilder builder, IcoCommonServiceClientSettings settings, ILog log)
        {
            builder.RegisterIcoCommonClient(settings?.ServiceUrl, log);
        }
    }
}
