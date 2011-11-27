using Ninject.Syntax;

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
                        kernel.Bind<IResolutionRoot>().ToConstant(kernel);
                        return kernel;
                    });
        }

        public override IExtensionResolver<ISensor> CreateExtensionResolver()
        {
            return this.standardKernel.Value.Get<IExtensionResolver<ISensor>>();
        }

        protected IKernel Kernel
        {
            get { return this.standardKernel.Value; }
        }

        protected override void DefineRunSyntax(ISyntaxBuilder<ISensor> builder)
        {
            builder
                .Begin
                    .With(() => this.Kernel.Get<ExtensionConfigurationSectionBehavior>())
                .Execute(() => GetVphtMessageBus(), (sensor, messagebus) => sensor.MessageBusInitialized(messagebus))
                .Execute(() => GetVphtDataBus(), (sensor, databus) => sensor.DataBusInitialized(databus))
                .Execute(sensor => sensor.Initialize())
                .Execute(sensor => sensor.StartObservation())
                    .With(() => this.Kernel.Get<IMakeAwareOfHeartbeat>())
                    .With(() => this.Kernel.Get<IStartHeartbeat>());
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
            if (this.standardKernel.IsValueCreated && !this.Kernel.IsDisposed)
            {
                this.Kernel.Dispose();
            }

            base.Dispose(disposing);
        }

        private IVphtMessageBus GetVphtMessageBus()
        {
            return this.Kernel.Get<IVphtMessageBus>();
        }

        private IVhptDataBus GetVphtDataBus()
        {
            return this.Kernel.Get<IVhptDataBus>();
        }
    }
}