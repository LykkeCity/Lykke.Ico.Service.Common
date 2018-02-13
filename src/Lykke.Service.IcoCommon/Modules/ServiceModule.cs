using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.JobTriggers.Extenstions;
using Lykke.Service.IcoCommon.AzureRepositories;
using Lykke.Service.IcoCommon.AzureRepositories.Mail;
using Lykke.Service.IcoCommon.AzureRepositories.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Lykke.Service.IcoCommon.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.IcoCommon.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<IcoCommonSettings> _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<IcoCommonSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            //  builder.RegisterType<QuotesPublisher>()
            //      .As<IQuotesPublisher>()
            //      .WithParameter(TypedParameter.From(_settings.CurrentValue.QuotesPublication))

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            builder.RegisterType<PayInAddressRepository>()
                .As<IPayInAddressRepository>()
                .WithParameter(TypedParameter.From(_settings.Nested(x => x.Db.DataConnString)));

            builder.RegisterType<EmailTemplateRepository>()
                .As<IEmailTemplateRepository>()
                .WithParameter(TypedParameter.From(_settings.Nested(x => x.Db.DataConnString)));

            builder.RegisterType<TransactionService>()
                .As<ITransactionService>()
                .WithParameter(TypedParameter.From(_settings.Nested(x => x.Campaigns)));

            builder.RegisterType<EmailTemplateService>()
                .As<IEmailTemplateService>()
                .SingleInstance();

            builder.RegisterType<EmailService>()
                .As<IEmailService>();

            RegisterAzureQueueHandlers(builder);

            // TODO: Add your dependencies here

            builder.Populate(_services);
        }

        private void RegisterAzureQueueHandlers(ContainerBuilder builder)
        {
            builder.AddTriggers(
                pool =>
                {
                    pool.AddDefaultConnection(_settings.Nested(x => x.Db.DataConnString));
                });
        }
    }
}
