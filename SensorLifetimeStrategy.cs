namespace bootstrapper.sample
{
    using System;
    using System.Reflection;
    using bbv.Common.Bootstrapper;
    using bbv.Common.Bootstrapper.Behavior;
    using bbv.Common.Bootstrapper.Configuration;
    using bbv.Common.Bootstrapper.Syntax;
    using bootstrapper.sample.Heartbeat;
    using bootstrapper.sample.Sirius;
    using Ninject;

    public class SensorLifetimeStrategy : AbstractStrategy<ISensor>
    {
        private readonly Lazy<IKernel> standardKernel;

        public SensorLifetimeStrategy()
        {
            this.standardKernel =
                new Lazy<IKernel>(() =>
                    {
                        var kernel = new StandardKernel();
                        kernel.Load(Assembly.GetExecutingAssembly());
                        return kernel;
                    });
        }

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            return this.standardKernel.Value.Get<IExtensionResolver<ISensor>>();
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Begin
                    .With(new ExtensionConfigurationSectionBehavior())
                .Execute(() => GetVphtMessageBus(), (sensor, messagebus) => sensor.MessageBusInitialized(messagebus))
                .Execute(() => GetVphtDataBus(), (sensor, databus) => sensor.DataBusInitialized(databus))
                .Execute(sensor => sensor.Initialize())
                .Execute(sensor => sensor.StartObservation())
                    .With(() => this.standardKernel.Value.Get<IMakeAwareOfHeartbeat>())
                    .With(() => this.standardKernel.Value.Get<IStartHeartbeat>());
        }

        protected override void DefineShutdownSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Execute(sensor => sensor.StopObservation())
                    .With(() => this.standardKernel.Value.Get<IStopHeartbeat>())
                .End
                    .With(new DisposeExtensionBehavior());
        }

        protected override void Dispose(bool disposing)
        {
            if (this.standardKernel.IsValueCreated && !this.standardKernel.Value.IsDisposed)
            {
                this.standardKernel.Value.Dispose();
            }

            base.Dispose(disposing);
        }

        private IVphtMessageBus GetVphtMessageBus()
        {
            return this.standardKernel.Value.Get<IVphtMessageBus>();
        }

        private IVhptDataBus GetVphtDataBus()
        {
            return this.standardKernel.Value.Get<IVhptDataBus>();
        }
    }
}